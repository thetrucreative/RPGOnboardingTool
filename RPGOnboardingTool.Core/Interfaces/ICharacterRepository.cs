using RPGOnboardingTool.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RPGOnboardingTool.Core.Interfaces
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        Task<Character?> GetByIdAsync(Guid id);
        Task<Character?> GetByIdAsync(Guid characterId, Guid userId);
        Task<IEnumerable<Character>> FindAsync(Expression<Func<Character, bool>> predicate);
        Task<IEnumerable<Character>> GetCharactersByUserIdAsync(Guid userId);
    }
}