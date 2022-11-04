using IM.UseCases.Dtos;
using IM.UseCases.Dtos.Enums;
using IM.UseCases.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IM.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _service;

        public CategoryController(ILogger<CategoryController> logger,
            ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListCategoriesAsync(int draw, int start, int length,
            string searchByName, StatusDto filterByStatus = 0)
        {
            string order = Request.Form["order[0][column]"][0];
            string orderDir = Request.Form["order[0][dir]"][0];

            var listCategoriesTuple = await _service
                .ListCategoriesWithSortingFilteringPagingServiceAsync(start, length,
                order, orderDir, searchByName, filterByStatus);

            int totalRecord = listCategoriesTuple.Item2;
            int filterRecord = listCategoriesTuple.Item3;
            List<object> listCategories = listCategoriesTuple.Item1;

            return Json(new
            {
                draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = listCategories
            });
        }

        private IEnumerable<CategoryDto> SortByColumnWithOrder(string order, string orderDir, IEnumerable<CategoryDto> data)
        {
            // Initialization.   
            IEnumerable<CategoryDto> sortedEntities = Enumerable.Empty<CategoryDto>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Name).ToList() : data.OrderBy(p => p.Name).ToList();
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Description).ToList() :
                            data.OrderBy(p => p.Description).ToList();
                        break;
                    case "2":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.Status).ToList() : 
                            data.OrderBy(p => p.Status).ToList();
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.Name).ToList() : 
                            data.OrderBy(p => p.Name).ToList();
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto entityDto)
        {
            // IDictionary<string, string> errors = _service.ValidateCategoryDtoService(entityDto);
            // if (errors.Count > 0) ModelState.Merge(errors);

            if (!ModelState.IsValid) return View(entityDto);
            _service.ValidateCategoryDtoService(entityDto);
            if (!await _service.CreateCategoryServiceAsync(entityDto)) return View(entityDto);
            TempData["success"] = "Category created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var entityDto = await _service.GetCategoryByIdServiceAsync(id);
            if (entityDto == null) return NotFound();
            return View(entityDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto entityDto)
        {
            if (!ModelState.IsValid) return View(entityDto);
            if (!await _service.UpdateCategoryServiceAsync(entityDto)) return View(entityDto);
            TempData["success"] = "Category updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            string devDeletePartial = "_CategoryDeletePartial";

            var entityDto = await _service.GetCategoryByIdServiceAsync(id);
            if (entityDto == null) return NotFound();

            return PartialView(devDeletePartial, entityDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            if (!await _service.DeleteCategoryByIdServiceAsync(id)) return NotFound();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var entityDto = await _service.GetCategoryByIdServiceAsync(id);
            if (entityDto == null) return NotFound();
            return View(entityDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}