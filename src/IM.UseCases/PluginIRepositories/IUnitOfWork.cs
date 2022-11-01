namespace IM.UseCases.PluginIRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }

        void Dispose();
        Task<bool> SaveAsync();
    }
}