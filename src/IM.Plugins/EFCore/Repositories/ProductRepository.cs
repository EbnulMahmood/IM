using IM.CoreBusiness.Entities;
using IM.CoreBusiness.Enums;
using IM.Plugins.EFCore.Data;
using IM.UseCases.PluginIRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IM.Plugins.EFCore.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly InventoryDbContext _context;
        private readonly ILogger _logger;

        public ProductRepository(InventoryDbContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        // search by name
        private async Task<(IEnumerable<Product>, int)> SearchProductsByName(string name, int start, int length)
        {
            var recordCount = await _context.Products
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            return ((await _context.Products
                        .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                        .Where(x => x.Status != Status.Deleted)
                        .Include(c => c.Category)
                        .Skip(start).Take(length)
                        .ToListAsync())
                        .Select(c => new Product()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Status = c.Status,
                        }), recordCount);
        }

        // filter by status
        private async Task<(IEnumerable<Product>, int)> FilterProductsByStatus(Status status, int start, int length)
        {
            var recordCount = await _context.Products
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status)
                );

            return ((await _context.Products.Where(x => x.Status == status)
                        .Where(x => x.Status != Status.Deleted)
                        .Include(c => c.Category)
                        .ToListAsync())
                        .Select(c => new Product()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Status = c.Status,
                        }), recordCount);
        }

        // filter by status and name
        private async Task<(IEnumerable<Product>, int)> ProductsByNameAndStatus(string name, Status status, int start, int length)
        {
            var recordCount = await _context.Products
                .CountAsync(x => (x.Status != Status.Deleted) &&
                    (x.Status == status) &&
                    (x.Name.ToLower().Contains(name.ToLower()))
                );

            return ((await _context.Products.Where(x => x.Status == status)
                        .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                        .Where(x => x.Status != Status.Deleted)
                        .Include(c => c.Category)
                        .Skip(start).Take(length)
                        .ToListAsync())
                        .Select(c => new Product()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Status = c.Status,
                        }), recordCount);
        }

        // list with paging
        private async Task<(IEnumerable<Product>, int)> ListProductsWithPaginationAsync(int start, int length)
        {
            // count records exclude deleted
            var recordCount = await _context.Products.CountAsync(x => x.Status != Status.Deleted);

            return (await (from product in _context.Products
                    join category in _context.Categories
                    on product.CategoryId equals category.Id
                    where product.Status != Status.Deleted
                    orderby product.CreatedAt descending
                    select product)
                    .Skip(start).Take(length)
                    .ToListAsync(), recordCount);

            // return ((await _context.Products
            //             .Where(d => d.Status != Status.Deleted)
            //             .OrderByDescending(d => d.CreatedAt)
            //             .Include(c => c.Category)
            //             .Select(c => new Product()
            //             {
            //                 Id = c.Id,
            //                 Name = c.Name,
            //                 Status = c.Status,
            //                 CategoryId = c.CategoryId,
            //                 Category = c.Category,
            //             })
            //             .Skip(start).Take(length)
            //             .ToListAsync())
            //             , recordCount);
        }

        // sort by order desc
        private IEnumerable<Product> SortByColumnWithOrder(string order, string orderDir, IEnumerable<Product> data)
        {
            // Initialization.   
            IEnumerable<Product> sortedEntities = Enumerable.Empty<Product>();

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
                            .Select(c => new Product()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) :
                            data.OrderBy(p => p.Name)
                            .ToList()
                            .Select(c => new Product()
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
                            .Select(c => new Product()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) :
                            data.OrderBy(p => p.Status)
                            .ToList()
                            .Select(c => new Product()
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
                            .Select(c => new Product()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }) :
                            data.OrderBy(p => p.CreatedAt)
                            .ToList()
                            .Select(c => new Product()
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

        public async Task<(IEnumerable<Product>, int, int)> ListProductsWithSortingFilteringPagingAsync(int start, int length,
            string order, string orderDir, string searchByName, Status filterByStatus = 0)
        {
            // get total count of data in table
            int totalRecord = await _context.Products.CountAsync();
            // filter record counter
            int filterRecord = 0;

            // Initialization.   
            IEnumerable<Product> listEntites = Enumerable.Empty<Product>();

            if (string.IsNullOrEmpty(searchByName) && filterByStatus == 0)
            {
                var listEntitesTuple = await ListProductsWithPaginationAsync(start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else if (filterByStatus == 0)
            {
                // search by Product name
                var listEntitesTuple = await SearchProductsByName(searchByName, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else if (string.IsNullOrEmpty(searchByName))
            {
                // filter by status
                var listEntitesTuple = await FilterProductsByStatus(filterByStatus, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }
            else
            {
                var listEntitesTuple = await ProductsByNameAndStatus(searchByName, filterByStatus, start, length);
                listEntites = listEntitesTuple.Item1;
                filterRecord = listEntitesTuple.Item2;
            }

            // Sorting 
            var result = SortByColumnWithOrder(order, orderDir, listEntites);

            return (result, totalRecord, filterRecord);
        }
    }
}