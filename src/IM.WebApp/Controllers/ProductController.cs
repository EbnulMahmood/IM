using IM.UseCases.Dtos.Enums;
using IM.UseCases.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IM.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

        public ProductController(ILogger<ProductController> logger,
            IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListProductsAsync(int draw, int start, int length,
            string searchByName, StatusDto filterByStatus = 0)
        {
            string order = Request.Form["order[0][column]"][0];
            string orderDir = Request.Form["order[0][dir]"][0];

            var listProductsTuple = await _service
                .ListProductsWithSortingFilteringPagingServiceAsync(start, length,
                order, orderDir, searchByName, filterByStatus);

            int totalRecord = listProductsTuple.Item2;
            int filterRecord = listProductsTuple.Item3;
            List<object> listProducts = listProductsTuple.Item1;

            return Json(new
            {
                draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = listProducts
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}