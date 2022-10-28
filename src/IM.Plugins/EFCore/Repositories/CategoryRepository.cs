using IM.CoreBusiness.Entities;
using IM.CoreBusiness.Enums;
using IM.Plugins.EFCore.Data;
using IM.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IM.Plugins.EFCore.Repositories
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private readonly InventoryDbContext _context;
        private bool disposed = false;

        public CategoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> ListCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid? id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) throw new Exception("Category does not exist in the database");

            return entity;
        }

        public async Task<bool> CreateCategoryAsync(Category entityToCreate)
        {
            if (await _context.Categories.AnyAsync(x => x.Name.ToLower()
                .Contains(entityToCreate.Name.ToLower())))
                throw new Exception($"{entityToCreate.Name} with same name already exists");

            await _context.Categories.AddAsync(entityToCreate);
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(Category entityToUpdate)
        {
            if (await _context.Categories.AnyAsync(x => x.Id != entityToUpdate.Id &&
                x.Name.ToLower().Equals(entityToUpdate.Name.ToLower())))
                throw new Exception($"{entityToUpdate.Name} with same name already exists");

            _context.Categories.Update(entityToUpdate);
            return true;
        }

        public async Task<bool> DeleteCategoryByIdAsync(Guid id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) throw new Exception("Category does not exist in the database");

            entity.Status = Status.Delete;
            _context.Categories.Update(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}