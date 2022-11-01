using System.Linq.Expressions;

namespace IM.UseCases.PluginIRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListEntitiesAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            string includeProperties = "");
        Task<TEntity?> GetEntityByIdAsync(object? id);
        Task<bool> CreateEntityAsync(TEntity entityToCreate);
        bool UpdateEntity(TEntity entityToUpdate);
        Task<bool> DeleteEntityAsync(object id);
        bool DeleteEntity(TEntity entityToDelete);
    }
}