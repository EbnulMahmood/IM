using IM.UseCases.Dtos.Enums;

namespace IM.UseCases.Services.Contracts
{
    public interface IProductService
    {
        Task<(List<object>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start,
            int length, string order, string orderDir, string searchByName,
            StatusDto filterByStatusDto = 0);
    }
}