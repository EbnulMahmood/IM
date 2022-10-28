using IM.CoreBusiness.Entities;

namespace IM.UseCases.PluginInterfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid? id);
        Task<bool> CreateCategoryAsync(Category entityToCreate);
        Task<bool> UpdateCategoryAsync(Category entityToUpdate);
        Task<bool> DeleteCategoryByIdAsync(Guid id);
        Task SaveAsync();
    }
}