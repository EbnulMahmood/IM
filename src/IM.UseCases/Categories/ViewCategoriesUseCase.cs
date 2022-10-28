using IM.UseCases.Categories.Contracts;
using IM.UseCases.Dtos;
using IM.UseCases.Extensions;
using IM.UseCases.PluginInterfaces;

namespace IM.UseCases.Categories
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