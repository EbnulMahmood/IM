using IM.CoreBusiness.Entities;
using IM.CoreBusiness.Enums;

namespace IM.UseCases.PluginIRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<(IEnumerable<Product>, int, int)> ListProductsWithSortingFilteringPagingAsync(int start, 
            int length, string order, string orderDir, string searchByName,
            Status filterByStatus = 0);
    }
}