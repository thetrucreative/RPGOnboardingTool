namespace RPGOnboardingTool.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //resouce management & save changes to the data store
        ICharacterRepository CharacterRepository { get; }
        Task<int> CompleteAsync();
    }
}
