using Inventory.Management.UseCases.Categories.Contracts;
using Inventory.Management.UseCases.Dtos;
using Inventory.Management.UseCases.Extensions;
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

        public async Task<IEnumerable<CategoryDto>> ExecuteAsync()
        {
            var entities = await _repository.ListCategoriesAsync();

            var entitiesDto = entities.ConvertToDto();
            return entitiesDto;
        }
    }
}