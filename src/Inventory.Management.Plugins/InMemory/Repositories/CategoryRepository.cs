using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Management.UseCases.PluginInterfaces;
using Inventory.Management.CoreBusiness.Entities;

namespace Inventory.Management.Plugins.InMemory.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private List<Category> _categories;

        public CategoryRepository()
        {
            _categories = new List<Category>()
            {
                new Category { Name = "Electronics", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Fruits", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Groceries", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Beverage", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Cosmetics", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Est ullamcorper", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Nisi est", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Semper feugiat", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Commodo quis", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Metus vulputate", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Faucibus pulvinar", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Massa sed", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Posuere lorem", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Auctor urna", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Eget nunc", CreatedBy = Guid.NewGuid(), Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
            };
        }

        public async Task<IEnumerable<Category>> ListCategoriesAsync()
        {
            return await Task.FromResult((IEnumerable<Category>)_categories);
        }

        public async Task<Category> GetCategoryByIdAsync(Guid? id)
        {
            var category = _categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return null;
            return await Task.FromResult(category);
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