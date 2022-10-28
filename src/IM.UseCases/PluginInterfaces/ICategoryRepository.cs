using IM.CoreBusiness.Entities;

namespace IM.UseCases.PluginInterfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid? id);
        Task<Category> CreateCategoryAsync(Category entityToCreate);
        Task<Category> UpdateCategoryAsync(Category entityToUpdate);
        Task<bool> DeleteCategoryByIdAsync(Guid id);
    }
}