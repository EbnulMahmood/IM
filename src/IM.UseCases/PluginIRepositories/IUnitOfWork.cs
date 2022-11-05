namespace IM.UseCases.PluginIRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        void Dispose();
        Task<bool> SaveAsync();
    }
}