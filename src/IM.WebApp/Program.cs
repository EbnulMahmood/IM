using IM.Plugins.EFCore.Data;
using IM.Plugins.EFCore.Repositories;
using IM.UseCases.Categories;
using IM.UseCases.Categories.Contracts;
using IM.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<InventoryDbContext>(options =>
{
    options.UseInMemoryDatabase("InventoryDb");
});

// Add repositories
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

// Add usecases
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();

var app = builder.Build();

// create database
var scope = app.Services.CreateScope();
var inventoryDbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
inventoryDbContext.Database.EnsureDeleted();
inventoryDbContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
