using RPGOnboardingTool.Core.Models;

namespace RPGOnboardingTool.Core.Interfaces
{
    //provides character data access ops
    public interface ICharacterRepository
    {
        Task<Character?> GetByIdAsync(Guid id, Guid userId);
        Task<Character?> GetByUserIdAsync(Guid userId);
        Task AddAsync(Character character);
        Task UpdateAsync(Character character);
        Task DeleteAsync(Character character);
    }
}
