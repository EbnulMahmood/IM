using IM.CoreBusiness.Enums;
using IM.UseCases.Dtos.Enums;
using IM.UseCases.Extensions;
using IM.UseCases.PluginIRepositories;
using IM.UseCases.Services.Contracts;

namespace IM.UseCases.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(List<object>, int, int)> ListProductsWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchByName, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listProductsTuple = await _unitOfWork.ProductRepository.ListProductsWithSortingFilteringPagingAsync(start, length, order, orderDir,
                    searchByName, (Status)filterByStatusDto);

                int totalRecord = listProductsTuple.Item2;
                int filterRecord = listProductsTuple.Item3;
                var listProductsDto = listProductsTuple.Item1.ConvertToDto();

                foreach (var item in listProductsTuple.Item1)
                {
                    Console.WriteLine(item.CategoryId);
                }

                List<object> entitiesList = new List<object>();
                foreach (var item in listProductsDto)
                {
                    List<string> dataItems = new List<string>
                    {
                        item.Name,
                        item.CategoryName,
                        item.StatusHtml,
                        item.ActionLinkHtml
                    };

                    entitiesList.Add(dataItems);
                }

                return (entitiesList, totalRecord, filterRecord);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}