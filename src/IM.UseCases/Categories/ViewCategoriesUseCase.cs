using IM.UseCases.Categories.Contracts;
using IM.UseCases.Dtos;
using IM.UseCases.Extensions;
using IM.UseCases.PluginInterfaces;

namespace IM.UseCases.Categories
{
    public class ViewCategoriesUseCase : IViewCategoriesUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewCategoriesUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDto>> ExecuteAsync()
        {
            var entities = await _unitOfWork.CategoryRepository.ListEntitiesAsync();

            var entitiesDto = entities.ConvertToDto();
            return entitiesDto;
        }
    }
}