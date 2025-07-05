// RPGOnboardingTool.Infrastructure/Repositories/CharacterRepository.cs
using Microsoft.EntityFrameworkCore;
using RPGOnboardingTool.Core.Interfaces;
using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RPGOnboardingTool.Infrastructure.Repositories
{
    public class CharacterRepository : GenericRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<Character?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.CharacterRace)
                .Include(c => c.CharacterTrainingPackage)
                .Include(c => c.Stats)
                .Include(c => c.Skills)
                .Include(c => c.CharacterTraits)
                    .ThenInclude(ct => ct.Trait)
                .Include(c => c.CharacterEquipment)
                    .ThenInclude(ce => ce.EquipmentItem)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character?> GetByIdAsync(Guid characterId, Guid userId)
        {
            return await _dbSet
                .Include(c => c.CharacterRace)
                .Include(c => c.CharacterTrainingPackage)
                .Include(c => c.Stats)
                .Include(c => c.Skills)
                .Include(c => c.CharacterTraits)
                    .ThenInclude(ct => ct.Trait)
                .Include(c => c.CharacterEquipment)
                    .ThenInclude(ce => ce.EquipmentItem)
                .FirstOrDefaultAsync(c => c.Id == characterId && c.UserId == userId);
        }

        public async Task<IEnumerable<Character>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.Name)
                .ToListAsync();
        }

        public new async Task<IEnumerable<Character>> FindAsync(Expression<Func<Character, bool>> predicate)
        {
            return await _context.Characters.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharactersByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(c => c.UserId == userId)
                .Include(c => c.CharacterRace)
                .Include(c => c.CharacterTrainingPackage)
                .ToListAsync();
        }
    }
}