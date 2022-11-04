using IM.UseCases.Dtos;
using IM.UseCases.Dtos.Enums;

namespace IM.UseCases.Services.Contracts
{
    public interface ICategoryService
    {
        IDictionary<string, string> ValidateCategoryDtoService(CategoryDto entityDto);
        Task<(List<object>, int, int)> ListCategoriesWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchByName, StatusDto filterByStatusDto = 0);
        Task<CategoryDto> GetCategoryByIdServiceAsync(Guid? entityDtoToGetId);
        Task<bool> CreateCategoryServiceAsync(CategoryDto entityDtoToCreate);
        Task<bool> UpdateCategoryServiceAsync(CategoryDto entityDtoToUpdate);
        Task<bool> DeleteCategoryByIdServiceAsync(Guid entityDtoToDeleteId);
    }
}