//// RPGOnboardingTool.Application/Services/CharacterService.cs
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RPGOnboardingTool.Application.DTOs;
using RPGOnboardingTool.Core.Interfaces;
using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Core.Enums;
using RPGOnboardingTool.Core.Models.Items;

namespace RPGOnboardingTool.Application.Services
{
    /// <summary>
    /// Service responsible for handling character creation, validation, and retrieval.
    /// Implements the game rules for character generation.
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
            Console.WriteLine($"🔄 CharacterService.CreateCharacterAsync started for: {characterDto.Name}");
            
            var race = await _unitOfWork.Races.GetByIdAsync(characterDto.RaceId);
            if (race == null)
            {
                Console.WriteLine($"❌ Invalid Race ID: {characterDto.RaceId}");
                throw new ArgumentException("Invalid Race ID");
            }

            Console.WriteLine($"✅ Race found: {race.Name}");

            var character = new Character(characterDto.UserId, characterDto.Name)
            {
                RaceId = characterDto.RaceId,
                TrainingPackageId = characterDto.TrainingPackageId,
                HasFinanceChip = characterDto.ChooseFinanceChip
            };

            Console.WriteLine($"✅ Character object created with ID: {character.Id}");

            // 1. STATS
            if (characterDto.AllocatedStats != null)
            {
                Console.WriteLine($"🎯 Adding {characterDto.AllocatedStats.Count} stats");
                foreach (var statDto in characterDto.AllocatedStats)
                {
                    character.Stats.Add(new Stat 
                    { 
                        Type = Enum.Parse<StatType>(statDto.Key), 
                        Value = statDto.Value,
                        CharacterId = character.Id // Explicitly set the foreign key
                    });
                }
                Console.WriteLine($"✅ Stats added: {character.Stats.Count}");
            }

            // 2. SKILLS
            if (characterDto.SelectedSkills != null)
            {
                Console.WriteLine($"🎯 Adding {characterDto.SelectedSkills.Count} skills");
                var allSkillDefs = await _unitOfWork.Skills.GetAllAsync();
                foreach (var skillDto in characterDto.SelectedSkills)
                {
                    var skillDef = allSkillDefs.FirstOrDefault(s => s.Name == skillDto.SkillName);
                    if (skillDef != null)
                    {
                        character.Skills.Add(new Skill 
                        { 
                            Name = skillDef.Name, 
                            Rank = skillDto.Rank, 
                            RelatedStat = skillDef.RelatedStat,
                            CharacterId = character.Id // Explicitly set the foreign key
                        });
                    }
                }
                Console.WriteLine($"✅ Skills added: {character.Skills.Count}");
            }

            // 3. TRAITS
            if (characterDto.SelectedTraits != null)
            {
                Console.WriteLine($"🎯 Adding {characterDto.SelectedTraits.Count} traits");
                foreach (var traitDto in characterDto.SelectedTraits)
                {
                    var trait = await _unitOfWork.Traits.GetByIdAsync(traitDto.TraitId);
                    if (trait != null)
                    {
                        character.CharacterTraits.Add(new CharacterTrait 
                        { 
                            TraitId = trait.Id, 
                            Rank = traitDto.Rank,
                            CharacterId = character.Id // Explicitly set the foreign key
                        });
                    }
                }
                Console.WriteLine($"✅ Traits added: {character.CharacterTraits.Count}");
            }

            // 4. EQUIPMENT & CREDITS
            int totalCost = 0;
            if (characterDto.SelectedEquipment != null)
            {
                Console.WriteLine($"🎯 Adding {characterDto.SelectedEquipment.Count} equipment items");
                foreach (var equipDto in characterDto.SelectedEquipment)
                {
                    var equipmentItem = await _unitOfWork.Equipment.GetByIdAsync(equipDto.EquipmentItemId);
                    if (equipmentItem != null)
                    {
                        character.CharacterEquipment.Add(new CharacterEquipment 
                        { 
                            EquipmentItemId = equipmentItem.Id, 
                            Quantity = equipDto.Quantity,
                            CharacterId = character.Id, // Explicitly set the foreign key
                            Character = character,
                            EquipmentItem = equipmentItem
                        });
                        totalCost += equipmentItem.Cost * equipDto.Quantity;
                    }
                }
                Console.WriteLine($"✅ Equipment added: {character.CharacterEquipment.Count}");
            }
            
            character.Credits -= totalCost;

            // Post-creation financial adjustments
            if (race.Name.ToLower() != "ebonite")
            {
                character.Credits += 100; // Finance Card
                character.Unis = 100;
            }

            Console.WriteLine($"💰 Final credits: {character.Credits}, Unis: {character.Unis}, Finance Chip: {character.HasFinanceChip}");

            Console.WriteLine("💾 Adding character to repository...");
            await _unitOfWork.Characters.AddAsync(character);
            
            Console.WriteLine("💾 Saving changes to database...");
            var saveResult = await _unitOfWork.CompleteAsync();
            Console.WriteLine($"✅ SaveChanges result: {saveResult} entities saved");

            Console.WriteLine($"✅ Character creation completed for: {character.Name} (ID: {character.Id})");

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
            Console.WriteLine($"🔍 Getting characters for user: {userId}");
            var characters = await _unitOfWork.Characters.GetCharactersByUserIdAsync(userId);
            Console.WriteLine($"📋 Found {characters.Count()} characters in database");

            var characterSummaries = characters.Select(character => new CharacterSummaryDto
            {
                Id = character.Id,
                Name = character.Name,
                RaceName = character.CharacterRace?.Name ?? "N/A",
                TrainingPackageName = character.CharacterTrainingPackage?.Name ?? "N/A"
            }).ToList();

            Console.WriteLine($"📋 Returning {characterSummaries.Count} character summaries");
            foreach (var summary in characterSummaries)
            {
                Console.WriteLine($"  - {summary.Name} (ID: {summary.Id}) - Race: {summary.RaceName}");
            }

            return characterSummaries;
        }

        public async Task UpdateCharacterAsync(Guid characterId, CharacterUpdateDto characterDto)
        {
            var characterResults = await _unitOfWork.Characters.FindAsync(c => c.Id == characterId);
            var character = characterResults.FirstOrDefault();
            if (character == null)
            {
                throw new KeyNotFoundException("Character not found.");
            }

            // Update basic properties
            character.Name = characterDto.Name;
            character.RaceId = characterDto.RaceId;
            character.TrainingPackageId = characterDto.TrainingPackageId;
            character.HasFinanceChip = characterDto.ChooseFinanceChip;
            character.AvatarUrl = characterDto.AvatarUrl;

            // Update stats
            character.Stats.Clear();
            if (characterDto.AllocatedStats != null)
            {
                foreach (var statDto in characterDto.AllocatedStats)
                {
                    character.Stats.Add(new Stat 
                    { 
                        Type = Enum.Parse<StatType>(statDto.Key), 
                        Value = statDto.Value,
                        CharacterId = character.Id // Explicitly set the foreign key
                    });
                }
            }

            // Update skills
            character.Skills.Clear();
            if (characterDto.SelectedSkills != null)
            {
                var allSkillDefs = await _unitOfWork.Skills.GetAllAsync();
                foreach (var skillDto in characterDto.SelectedSkills)
                {
                    var skillDef = allSkillDefs.FirstOrDefault(s => s.Name == skillDto.SkillName);
                    if (skillDef != null)
                    {
                        character.Skills.Add(new Skill 
                        { 
                            Name = skillDef.Name, 
                            Rank = skillDto.Rank, 
                            RelatedStat = skillDef.RelatedStat,
                            CharacterId = character.Id // Explicitly set the foreign key
                        });
                    }
                }
            }

            // Update traits
            character.CharacterTraits.Clear();
            if (characterDto.SelectedTraits != null)
            {
                foreach (var traitDto in characterDto.SelectedTraits)
                {
                    var trait = await _unitOfWork.Traits.GetByIdAsync(traitDto.TraitId);
                    if (trait != null)
                    {
                        character.CharacterTraits.Add(new CharacterTrait 
                        { 
                            TraitId = trait.Id, 
                            Rank = traitDto.Rank,
                            CharacterId = character.Id // Explicitly set the foreign key
                        });
                    }
                }
            }

            // Update equipment
            character.CharacterEquipment.Clear();
            if (characterDto.SelectedEquipment != null)
            {
                foreach (var equipDto in characterDto.SelectedEquipment)
                {
                    var equipmentItem = await _unitOfWork.Equipment.GetByIdAsync(equipDto.EquipmentItemId);
                    if (equipmentItem != null)
                    {
                        character.CharacterEquipment.Add(new CharacterEquipment 
                        { 
                            EquipmentItemId = equipmentItem.Id, 
                            Quantity = equipDto.Quantity,
                            CharacterId = character.Id, // Explicitly set the foreign key
                            Character = character,
                            EquipmentItem = equipmentItem
                        });
                    }
                }
            }

            // Update general items
            character.CharacterGeneralItems.Clear();
            if (characterDto.SelectedGeneralItems != null)
            {
                foreach (var generalItemDto in characterDto.SelectedGeneralItems)
                {
                    var generalItem = await _unitOfWork.GeneralItems.GetByIdAsync(generalItemDto.GeneralItemId);
                    if (generalItem != null)
                    {
                        character.CharacterGeneralItems.Add(new CharacterGeneralItem 
                        { 
                            GeneralItemId = generalItem.Id, 
                            Quantity = generalItemDto.Quantity,
                            CharacterId = character.Id // Explicitly set the foreign key
                        });
                    }
                }
            }

            _unitOfWork.Characters.Update(character);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCharacterAsync(Guid characterId, Guid userId)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(characterId, userId);
            if (character == null)
            {
                throw new KeyNotFoundException("Character not found.");
            }

            _unitOfWork.Characters.Remove(character);
            await _unitOfWork.CompleteAsync();
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

        public async Task<IEnumerable<GeneralItem>> GetAllGeneralItemsAsync()
        {
            return await _unitOfWork.GeneralItems.GetAllAsync();
        }

        public async Task<IEnumerable<SkillDefinition>> GetAllSkillsAsync()
        {
            return await _unitOfWork.Skills.GetAllAsync();
        }

        public async Task<Character?> GetCharacterForDetailByIdAsync(Guid characterId)
        {
            return await _unitOfWork.Characters.GetByIdAsync(characterId);
        }

        public async Task UpdateCharacterAvatarAsync(Guid characterId, IFormFile avatarFile)
        {
            var characterResults = await _unitOfWork.Characters.FindAsync(c => c.Id == characterId);
            var character = characterResults.FirstOrDefault();
            if (character == null)
            {
                throw new KeyNotFoundException("Character not found.");
            }

            if (avatarFile != null && avatarFile.Length > 0)
            {
                var fileName = $"{characterId}_{Path.GetRandomFileName()}{Path.GetExtension(avatarFile.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatars", fileName);

                // Create directory if it doesn't exist
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatarFile.CopyToAsync(stream);
                }

                // Delete old avatar if it exists
                if (!string.IsNullOrEmpty(character.AvatarUrl))
                {
                    var oldAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", character.AvatarUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (File.Exists(oldAvatarPath))
                    {
                        File.Delete(oldAvatarPath);
                    }
                }

                character.AvatarUrl = $"/images/avatars/{fileName}";
                _unitOfWork.Characters.Update(character);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}