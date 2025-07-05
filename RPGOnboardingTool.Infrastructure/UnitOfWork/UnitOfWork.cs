using RPGOnboardingTool.Core.Interfaces;
using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Infrastructure.Data;
using RPGOnboardingTool.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RPGOnboardingTool.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICharacterRepository Characters { get; }
        public IGenericRepository<Race> Races { get; }
        public IGenericRepository<TrainingPackage> TrainingPackages { get; }
        public IGenericRepository<Trait> Traits { get; }
        public IGenericRepository<EquipmentItem> Equipment { get; }
        public IGenericRepository<SkillDefinition> Skills { get; }

        public UnitOfWork(ApplicationDbContext context, ICharacterRepository characterRepository)
        {
            _context = context;
            Characters = characterRepository;
            Races = new GenericRepository<Race>(_context);
            TrainingPackages = new GenericRepository<TrainingPackage>(_context);
            Traits = new GenericRepository<Trait>(_context);
            Equipment = new GenericRepository<EquipmentItem>(_context);
            Skills = new GenericRepository<SkillDefinition>(_context);
        }

        public void SetEntityRowVersion<T>(T entity, byte[] rowVersion) where T : class
        {
            if (entity != null && rowVersion != null && rowVersion.Length > 0)
            {
                var entry = _context.Entry(entity);
                entry.Property("RowVersion").OriginalValue = rowVersion;
            }
        }

        /// <summary>
        /// Saves all changes made in this unit of work to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the underlying DbContext. This method is part of IDisposable.
        /// </summary>
        public void Dispose() 
        {
            _context.Dispose();
        }
    }
}