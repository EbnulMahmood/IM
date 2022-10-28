using System.Linq.Expressions;
using IM.Plugins.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace IM.Plugins.EFCore.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private readonly InventoryDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(InventoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> ListEntitiesAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
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

        public virtual async Task<TEntity> GetEntityByIdAsync(object? id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) 
                throw new Exception($"{nameof(entity)} does not exist in the database");

            return entity;
        }

        public virtual async Task CreateEntityAsync(TEntity entityToCreate)
        {
            await _dbSet.AddAsync(entityToCreate);
        }

        public virtual void UpdateEntity(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
        }

        public virtual async Task DeleteEntityAsync(object id)
        {
            TEntity entity = await _dbSet.FindAsync(id) ?? 
                throw new Exception("Entity does not exist in the database");

            DeleteEntity(entity);
        }

        public virtual void DeleteEntity(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
    }
}