using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IM.WebApp.Models;
using IM.UseCases.Categories.Contracts;

namespace IM.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IViewCategoriesUseCase viewCategoriesUseCase,
        ILogger<HomeController> logger)
    {
        _viewCategoriesUseCase = viewCategoriesUseCase;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var entities = await _viewCategoriesUseCase.ExecuteAsync();
        return View(entities);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
