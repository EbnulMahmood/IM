using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Management.CoreBusiness.Entities;

namespace Inventory.Management.UseCases.PluginInterfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(long? entityToGetId);
        Task<Category> CreateCategoryAsync(Category entityToCreate);
        Task<Category> UpdateCategoryAsync(Category entityToUpdate);
        Task<bool> DeleteCategoryByIdAsync(long entityToDeleteId);
    }
}