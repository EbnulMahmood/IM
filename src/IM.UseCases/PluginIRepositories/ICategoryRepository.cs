using IM.CoreBusiness.Entities;
using IM.CoreBusiness.Enums;

namespace IM.UseCases.PluginIRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> ListCategoriesAsync(string name);
        Task<(IEnumerable<Category>, int, int)> ListCategoriesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0);
    }
}