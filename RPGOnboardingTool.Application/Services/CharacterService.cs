//// RPGOnboardingTool.Application/Services/CharacterService.cs
using RPGOnboardingTool.Application.DTOs;
using RPGOnboardingTool.Core.Interfaces;
using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace RPGOnboardingTool.Application.Services
{
    /// <summary>
    /// Service responsible for handling character creation, validation, and retrieval.
    /// Implements the game rules for character generation.
    /// </summary>
    public class CharacterService : ICharacterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CharacterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new character based on the provided creation DTO and applies game rules.
        /// </summary>
        /// <param name="characterDto">The CharacterCreationDto containing user choices.</param>
        /// <returns>A CharacterResponseDto of the newly created character, or null if creation fails.</returns>
        /// <exception cref="ArgumentException">Thrown if validation fails.</exception>
        public async Task<CharacterResponseDto> CreateCharacterAsync(CharacterCreationDto characterDto)
        {
            var race = await _unitOfWork.Races.GetByIdAsync(characterDto.RaceId);
            if (race == null)
            {
                throw new ArgumentException("Invalid Race ID");
            }

            var character = new Character(characterDto.UserId, characterDto.Name, race)
            {
                TrainingPackageId = characterDto.TrainingPackageId,
                HasFinanceChip = characterDto.ChooseFinanceChip
            };

            // 1. STATS
            if (characterDto.AllocatedStats != null)
            {
                foreach (var statDto in characterDto.AllocatedStats)
                {
                    character.Stats.Add(new Stat { Type = Enum.Parse<StatType>(statDto.Key), Value = statDto.Value });
                }
            }

            // 2. SKILLS
            if (characterDto.SelectedSkills != null)
            {
                var allSkillDefs = await _unitOfWork.Skills.GetAllAsync();
                foreach (var skillDto in characterDto.SelectedSkills)
                {
                    var skillDef = allSkillDefs.FirstOrDefault(s => s.Name == skillDto.SkillName);
                    if (skillDef != null)
                    {
                        character.Skills.Add(new Skill { Name = skillDef.Name, Rank = skillDto.Rank, RelatedStat = skillDef.RelatedStat });
                    }
                }
            }

            // 3. TRAITS
            if (characterDto.SelectedTraits != null)
            {
                foreach (var traitDto in characterDto.SelectedTraits)
                {
                    var trait = await _unitOfWork.Traits.GetByIdAsync(traitDto.TraitId);
                    if (trait != null)
                    {
                        character.CharacterTraits.Add(new CharacterTrait { TraitId = trait.Id, Rank = traitDto.Rank });
                    }
                }
            }

            // 4. EQUIPMENT & CREDITS
            int totalCost = 0;
            if (characterDto.SelectedEquipment != null)
            {
                foreach (var equipDto in characterDto.SelectedEquipment)
                {
                    var equipmentItem = await _unitOfWork.Equipment.GetByIdAsync(equipDto.EquipmentItemId);
                    if (equipmentItem != null)
                    {
                        character.CharacterEquipment.Add(new CharacterEquipment { EquipmentItemId = equipmentItem.Id, Quantity = equipDto.Quantity });
                        totalCost += equipmentItem.Cost * equipDto.Quantity;
                    }
                }
            }
            if (character.HasFinanceChip)
            {
                totalCost += 100; // Cost of finance chip
            }
            character.Credits -= totalCost;


            await _unitOfWork.Characters.AddAsync(character);
            await _unitOfWork.CompleteAsync();

            return new CharacterResponseDto
            {
                Id = character.Id,
                Name = character.Name,
            };
        }

        public async Task<CharacterResponseDto?> GetCharacterByIdAsync(Guid characterId, Guid userId)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(characterId, userId);
            if (character == null) return null;

            return new CharacterResponseDto
            {
                Id = character.Id,
                Name = character.Name,
            };
        }

        public async Task<List<CharacterSummaryDto>> GetCharactersByUserIdAsync(Guid userId)
        {
            var characters = await _unitOfWork.Characters.GetCharactersByUserIdAsync(userId);

            var characterSummaries = characters.Select(character => new CharacterSummaryDto
            {
                Id = character.Id,
                Name = character.Name,
                RaceName = character.CharacterRace?.Name ?? "N/A",
                TrainingPackageName = character.CharacterTrainingPackage?.Name ?? "N/A"
            }).ToList();

            return characterSummaries;
        }

        public async Task<Character?> GetCharacterForDetailByIdAsync(Guid characterId)
        {
            return await _unitOfWork.Characters.GetByIdAsync(characterId);
        }

        public async Task UpdateCharacterAsync(Guid characterId, CharacterUpdateDto characterDto)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(characterId);
            if (character != null)
            {
                if (character.UserId != characterDto.UserId)
                {
                    throw new UnauthorizedAccessException("User is not authorized to update this character.");
                }

                // Convert the Base64 string RowVersion from the DTO back to a byte array for EF Core.
                if (!string.IsNullOrEmpty(characterDto.RowVersion))
                {
                    _unitOfWork.SetEntityRowVersion(character, Convert.FromBase64String(characterDto.RowVersion));
                }

                character.Name = characterDto.Name;
                character.RaceId = characterDto.RaceId;
                character.TrainingPackageId = characterDto.TrainingPackageId;
                character.HasFinanceChip = characterDto.ChooseFinanceChip;
                character.StatPointsRemaining = characterDto.StatPointsRemaining;
                character.SkillPointsRemaining = characterDto.SkillPointsRemaining;

                character.Stats.Clear();
                character.Skills.Clear();
                character.CharacterTraits.Clear();
                character.CharacterEquipment.Clear();

                if (characterDto.AllocatedStats != null)
                {
                    foreach (var statDto in characterDto.AllocatedStats)
                    {
                        character.Stats.Add(new Stat { Type = Enum.Parse<StatType>(statDto.Key), Value = statDto.Value });
                    }
                }

                if (characterDto.SelectedSkills != null)
                {
                    var allSkillDefs = await _unitOfWork.Skills.GetAllAsync();
                    foreach (var skillDto in characterDto.SelectedSkills)
                    {
                        var skillDef = allSkillDefs.FirstOrDefault(s => s.Name == skillDto.SkillName);
                        if (skillDef != null)
                        {
                            character.Skills.Add(new Skill { Name = skillDef.Name, Rank = skillDto.Rank, RelatedStat = skillDef.RelatedStat });
                        }
                    }
                }

                if (characterDto.SelectedTraits != null)
                {
                    foreach (var traitDto in characterDto.SelectedTraits)
                    {
                        character.CharacterTraits.Add(new CharacterTrait { TraitId = traitDto.TraitId, Rank = traitDto.Rank });
                    }
                }

                if (characterDto.SelectedEquipment != null)
                {
                    foreach (var equipDto in characterDto.SelectedEquipment)
                    {
                        var equipmentItem = await _unitOfWork.Equipment.GetByIdAsync(equipDto.EquipmentItemId);
                        if (equipmentItem != null)
                        {
                            character.CharacterEquipment.Add(new CharacterEquipment { EquipmentItemId = equipmentItem.Id, Quantity = equipDto.Quantity });
                        }
                    }
                }
                
                character.Credits = characterDto.Credits;

                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task DeleteCharacterAsync(Guid characterId, Guid userId)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(characterId, userId);
            if (character != null)
            {
                _unitOfWork.Characters.Remove(character);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {
            return await _unitOfWork.Races.GetAllAsync(r => r.StatLimits, r => r.SpeciesSkills);
        }

        public async Task<IEnumerable<TrainingPackage>> GetAllTrainingPackagesAsync()
        {
            return await _unitOfWork.TrainingPackages.GetAllAsync(tp => tp.PackageSkills);
        }

        public async Task<IEnumerable<Trait>> GetAllTraitsAsync()
        {
            return await _unitOfWork.Traits.GetAllAsync();
        }

        public async Task<IEnumerable<EquipmentItem>> GetAllEquipmentAsync()
        {
            return await _unitOfWork.Equipment.GetAllAsync();
        }

        public async Task<IEnumerable<SkillDefinition>> GetAllSkillsAsync()
        {
            return await _unitOfWork.Skills.GetAllAsync();
        }
    }
}