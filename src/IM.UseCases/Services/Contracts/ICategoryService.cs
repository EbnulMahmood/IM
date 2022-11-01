using IM.UseCases.Dtos;

namespace IM.UseCases.Services.Contracts
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryServiceAsync(CategoryDto entityDtoToCreate);
        Task<bool> DeleteCategoryByIdServiceAsync(Guid entityDtoToDeleteId);
        Task<CategoryDto> GetCategoryByIdServiceAsync(Guid? entityDtoToGetId);
        Task<IEnumerable<CategoryDto>> ListCategoriesServiceAsync();
        Task<bool> UpdateCategoryServiceAsync(CategoryDto entityDtoToUpdate);
        IDictionary<string, string> ValidateCategoryDtoService(CategoryDto entityDto);
    }
}