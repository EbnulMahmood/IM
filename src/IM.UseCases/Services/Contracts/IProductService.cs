using IM.UseCases.Dtos;
using IM.UseCases.Dtos.Enums;

namespace IM.UseCases.Services.Contracts
{
    public interface IProductService
    {
        Task<(List<object>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start,
            int length, string order, string orderDir, string searchByName,
            StatusDto filterByStatusDto = 0);
        IDictionary<string, string> ValidateProductDtoService(ProductDto entityDto);
        Task<bool> CreateProductServiceAsync(ProductDto entityDtoToCreate);
        Task<List<CategoryDto>> ListCategoriesSearch(string name);
    }
}