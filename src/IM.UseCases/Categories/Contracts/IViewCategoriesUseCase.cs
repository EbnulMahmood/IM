using IM.UseCases.Dtos;

namespace IM.UseCases.Categories.Contracts
{
    public interface IViewCategoriesUseCase
    {
        Task<IEnumerable<CategoryDto>> ExecuteAsync();
    }
}