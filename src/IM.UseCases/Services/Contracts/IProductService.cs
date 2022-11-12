using IM.UseCases.Dtos;
using IM.UseCases.Dtos.Enums;

namespace IM.UseCases.Services.Contracts
{
    public interface IProductService
    {
        Task<(List<ProductViewDto>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start,
            int length, string order, string orderDir, string searchByName,
            StatusDto filterByStatusDto = 0);
        Task<bool> CreateProductServiceAsync(ProductDto entityDtoToCreate);
        Task<List<CategoryDto>> ListCategoriesSearch(string name);
        Task<object> ListCategoriesServiceAsync(string name, int page
            , int resultCount);
        Task<IEnumerable<CategoryDto>> ListCategoriesServiceAsync();
    }
}