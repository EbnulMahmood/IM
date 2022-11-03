using IM.CoreBusiness.Entities;
using IM.CoreBusiness.Enums;
using IM.Plugins.EFCore.Data;
using IM.UseCases.PluginIRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IM.Plugins.EFCore.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly InventoryDbContext _context;
        private readonly ILogger _logger;

        public CategoryRepository(InventoryDbContext context, ILogger logger) :
            base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        // search by name
        private async Task<(IEnumerable<Category>, int)> SearchCategoriesByName(string name, int start, int length)
        {
            var recordCount = await _context.Categories
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            return ((await _context.Categories
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .Where(x => x.Status != Status.Deleted)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    }), recordCount);
        }

        // filter by status
        private async Task<(IEnumerable<Category>, int)> FilterCategoriesByStatus(Status status, int start, int length)
        {
            var recordCount = await _context.Categories
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status)
                );
            
            return ((await _context.Categories.Where(x => x.Status == status)
                    .Where(x => x.Status != Status.Deleted)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    }), recordCount);
        }

        // filter by status and name
        private async Task<(IEnumerable<Category>, int)> CategoriesByNameAndStatus(string name, Status status, int start, int length)
        {
            var recordCount = await _context.Categories
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            return ((await _context.Categories.Where(x => x.Status == status)
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .Where(x => x.Status != Status.Deleted)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    }), recordCount);
        }

        // list with paging
        private async Task<(IEnumerable<Category>, int)> ListCategoriesWithPaginationAsync(int start, int length)
        {
            // count records exclude deleted
            var recordCount = await _context.Categories.CountAsync(x => x.Status != Status.Deleted);

            var categories = (await _context.Categories.Where(d => d.Status != Status.Deleted)
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip(start).Take(length)
                    .ToListAsync())
                    .Select(c => new Category()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                    });
            
            return (categories, recordCount);
        }

        // sort by order desc
        private IEnumerable<Category> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Category> data)
        {
            // Initialization.   
            IEnumerable<Category> sortedEntities = Enumerable.Empty<Category>();

            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Name)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) :
                            data.OrderBy(p => p.Name)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            });
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.Status)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) : 
                            data.OrderBy(p => p.Status)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            });
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.CreatedAt)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) : 
                            data.OrderBy(p => p.CreatedAt)
                            .ToList()
                            .Select(c => new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            });
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.   
                Console.Write(ex);
            }
            // info.   
            return sortedEntities;
        }

        public async Task<(IEnumerable<Category>, int, int)> ListCategoriesWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Categories.CountAsync();
            // filter record counter
            int filterRecord = 0;

            // Initialization.   
            IEnumerable<Category> listCategories = Enumerable.Empty<Category>();
            bool isFiltered = false;

            if (!string.IsNullOrEmpty(searchByName) && filterByStatus != 0)
            {
                var listCategoriesTuple = await CategoriesByNameAndStatus(searchByName, filterByStatus, start, length);
                listCategories = listCategoriesTuple.Item1;
                filterRecord = listCategoriesTuple.Item2;

                isFiltered = true;
            }

            // search by category name
            else if (!string.IsNullOrEmpty(searchByName))
            {
                var listCategoriesTuple = await SearchCategoriesByName(searchByName, start, length);
                listCategories = listCategoriesTuple.Item1;
                filterRecord = listCategoriesTuple.Item2;

                isFiltered = true;
            }

            // filter by status
            else if (filterByStatus != 0)
            {
                var listCategoriesTuple = await FilterCategoriesByStatus(filterByStatus, start, length);
                listCategories = listCategoriesTuple.Item1;
                filterRecord = listCategoriesTuple.Item2;

                isFiltered = true;
            }

            if (!isFiltered)
            {
                var listCategoriesTuple = await ListCategoriesWithPaginationAsync(start, length);

                listCategories = listCategoriesTuple.Item1;

                // get total count of records
                filterRecord = listCategoriesTuple.Item2;
            }

            // Sorting 
            var result = SortByColumnWithOrder(order, orderDir, listCategories);

            return (result, totalRecord, filterRecord);
        }

    }
}