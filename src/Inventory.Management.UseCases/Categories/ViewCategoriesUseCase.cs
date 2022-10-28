using Inventory.Management.CoreBusiness.Entities;
using Inventory.Management.UseCases.Categories.Contracts;
using Inventory.Management.UseCases.PluginInterfaces;

namespace Inventory.Management.UseCases.Categories
{
    public class ViewCategoriesUseCase : IViewCategoriesUseCase
    {
        private readonly ICategoryRepository _repository;

        public ViewCategoriesUseCase(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> ExecuteAsync()
        {
            return await _repository.ListCategoriesAsync();
        }
    }
}