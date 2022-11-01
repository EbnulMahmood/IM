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
            string filter_keywords, StatusDto filter_option = 0)
        {
            var entities = await _service.ListCategoriesServiceAsync();
            int totalRecord = 0;
            int filterRecord = 0;

            string order = Request.Form["order[0][column]"][0];
            string orderDir = Request.Form["order[0][dir]"][0];

            //get total count of data in table
            totalRecord = entities.Count();

            if (!string.IsNullOrEmpty(filter_keywords))
            {
                entities = entities.Where(d => d.Name.ToLower().Contains(filter_keywords.ToLower()))
                .Where(d => d.Status != StatusDto.Deleted);
            }
            if (filter_option != 0)
            {
                entities = entities.Where(d => d.Status == filter_option)
                .Where(d => d.Status != StatusDto.Deleted);
            }

            // Sorting.   
            entities = SortByColumnWithOrder(order, orderDir, entities);

            // get total count of records after search 
            filterRecord = entities.Count();

            //pagination
            IEnumerable<CategoryDto> paginatdEntities = entities.Skip(start).Take(length)
                .OrderByDescending(d => d.CreatedAt).ToList()
                .Where(d => d.Status != StatusDto.Deleted);

            List<object> entitiesList = new List<object>();
            foreach (var item in paginatdEntities)
            {
                string actionLink = $"<div class='w-75 btn-group' role='group'>" +
                    $"<a href='Edit/{item.Id}' class='btn btn-primary mx-2'><i class='bi bi-pencil-square'></i>Edit</a>" +
                    $"<button type='button' data-bs-target='#deleteCategory' data-bs-toggle='ajax-modal' class='btn btn-danger mx-2 btn-category-delete'" +
                    $"data-category-id='{item.Id}'><i class='bi bi-trash-fill'></i>Delete</button><a href='Details/{item.Id}'" +
                    $"class='btn btn-secondary mx-2'><i class='bi bi-ticket-detailed-fill'></i>Details</a></div>";

                string statusConditionClass = item.Status == StatusDto.Active ? "text-success" : "text-danger";
                string statusConditionText = item.Status == StatusDto.Active ? "Active" : "Inactive";
                string status = $"<span class='{statusConditionClass}'>{statusConditionText}</span>";

                List<string> dataItems = new List<string>
                {
                    item.Name,
                    status,
                    actionLink
                };

                entitiesList.Add(dataItems);
            }

            return Json(new
            {
                draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = entitiesList
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
            if (!ModelState.IsValid) return View(entityDto);
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