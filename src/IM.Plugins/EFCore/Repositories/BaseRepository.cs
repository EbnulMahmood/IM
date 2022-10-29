using System.Linq.Expressions;
using IM.Plugins.EFCore.Data;
using IM.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IM.Plugins.EFCore.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly InventoryDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;

        public BaseRepository(InventoryDbContext context, ILogger logger)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _logger = logger;
        }

        public virtual async Task<IEnumerable<TEntity>> ListEntitiesAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(BaseRepository<TEntity>)} ListEntities function error");
                return new List<TEntity>();
            }
        }

        public virtual async Task<TEntity?> GetEntityByIdAsync(object? id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    throw new Exception($"{nameof(entity)} does not exist in the database");

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(BaseRepository<TEntity>)} GetEntity function error");
                return null;
            }
        }

        public virtual async Task<bool> CreateEntityAsync(TEntity entityToCreate)
        {
            try
            {
                await _dbSet.AddAsync(entityToCreate);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(BaseRepository<TEntity>)} CreateEntity function error");
                return false;
            }
        }

        public virtual bool UpdateEntity(TEntity entityToUpdate)
        {
            try
            {
                _dbSet.Update(entityToUpdate);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(BaseRepository<TEntity>)} UpdateEntity function error");
                return false;
            }
        }

        public virtual async Task<bool> DeleteEntityAsync(object id)
        {
            try
            {
                TEntity entity = await _dbSet.FindAsync(id) ??
                    throw new Exception("Entity does not exist in the database");

                return DeleteEntity(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(BaseRepository<TEntity>)} DeleteEntity function error");
                return false;
            }
        }

        public virtual bool DeleteEntity(TEntity entityToDelete)
        {
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}