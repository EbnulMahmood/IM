using Inventory.Management.CoreBusiness.Entities;
using Inventory.Management.Plugins.EFCore.Data;
using Inventory.Management.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Management.Plugins.EFCore.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryDbContext _context;

        public CategoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> ListCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<Category> GetCategoryByIdAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> CreateCategoryAsync(Category entityToCreate)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategoryAsync(Category entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategoryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}