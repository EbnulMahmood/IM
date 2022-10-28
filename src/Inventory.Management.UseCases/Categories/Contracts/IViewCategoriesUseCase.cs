using Inventory.Management.UseCases.Dtos;

namespace Inventory.Management.UseCases.Categories.Contracts
{
    public interface IViewCategoriesUseCase
    {
        Task<IEnumerable<CategoryDto>> ExecuteAsync();
    }
}