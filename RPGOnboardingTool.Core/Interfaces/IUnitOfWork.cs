using RPGOnboardingTool.Core.Models;
using RPGOnboardingTool.Core.Models.Items;

namespace RPGOnboardingTool.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICharacterRepository Characters { get; }
        IGenericRepository<Race> Races { get; }
        IGenericRepository<TrainingPackage> TrainingPackages { get; }
        IGenericRepository<Trait> Traits { get; }
        IGenericRepository<Core.Models.Items.EquipmentItem> Equipment { get; }
        IGenericRepository<GeneralItem> GeneralItems { get; }
        IGenericRepository<SkillDefinition> Skills { get; }
        void SetEntityRowVersion<T>(T entity, byte[] rowVersion) where T : class;
        Task<int> CompleteAsync();
    }
}