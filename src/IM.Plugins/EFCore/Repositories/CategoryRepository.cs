using IM.CoreBusiness.Entities;
using IM.Plugins.EFCore.Data;
using IM.UseCases.PluginIRepositories;
using Microsoft.Extensions.Logging;

namespace IM.Plugins.EFCore.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(InventoryDbContext context, ILogger logger) :
            base(context, logger)
        {
        }
    }
}