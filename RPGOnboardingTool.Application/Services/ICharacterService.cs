using RPGOnboardingTool.Application.DTOs;
using RPGOnboardingTool.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPGOnboardingTool.Application.Services
{
    public interface ICharacterService
    {
        Task<CharacterResponseDto> CreateCharacterAsync(CharacterCreationDto characterDto);
        Task<CharacterResponseDto?> GetCharacterByIdAsync(Guid characterId, Guid userId);
        Task<List<CharacterSummaryDto>> GetCharactersByUserIdAsync(Guid userId);
        Task UpdateCharacterAsync(Guid characterId, CharacterUpdateDto characterDto);
        Task DeleteCharacterAsync(Guid characterId, Guid userId);
        Task<IEnumerable<Race>> GetAllRacesAsync();
        Task<IEnumerable<TrainingPackage>> GetAllTrainingPackagesAsync();
        Task<IEnumerable<Trait>> GetAllTraitsAsync();
        Task<IEnumerable<EquipmentItem>> GetAllEquipmentAsync();
        Task<IEnumerable<SkillDefinition>> GetAllSkillsAsync();
        Task<Character?> GetCharacterForDetailByIdAsync(Guid characterId);
    }
}