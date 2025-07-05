using RPGOnboardingTool.Core.Models;

namespace RPGOnboardingTool.Core.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAllAsync();
        Task<Race> GetByIdAsync(int id);
        void Add(Race race);
        void Remove(Race race);
    }
}