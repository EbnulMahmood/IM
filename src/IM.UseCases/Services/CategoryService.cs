using IM.CoreBusiness.Enums;
using IM.UseCases.Dtos;
using IM.UseCases.Dtos.Enums;
using IM.UseCases.Extensions;
using IM.UseCases.PluginIRepositories;
using IM.UseCases.Services.Contracts;

namespace IM.UseCases.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IDictionary<string, string> ValidateCategoryDtoService(CategoryDto entityDto)
        {
            Guard.AgainstNullParameter(entityDto, nameof(entityDto));

            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (entityDto.Name.Trim().Length == 0)
                errors.Add("Name", "Name is required.");
            if (entityDto.Description?.Trim().Length == 0)
                errors.Add("Description", "Description is required.");
            return errors;
        }

        public async Task<IEnumerable<CategoryDto>> ListCategoriesServiceAsync(string name)
        {
            try
            {
                var entities = await _unitOfWork.CategoryRepository.ListCategoriesAsync(name);
                
                var entitiesDto = from category in entities select
                new CategoryDto
                {
                    Name = category.Name,
                };

                return entitiesDto;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<(List<CategoryViewDto>, int, int)> ListCategoriesWithSortingFilteringPagingServiceAsync(int start, int length,
            string order, string orderDir, string searchByName, StatusDto filterByStatusDto = 0)
        {
            try
            {
                var listCategoriesTuple = await _unitOfWork.CategoryRepository.ListCategoriesWithSortingFilteringPagingAsync(start, length, order, orderDir,
                    searchByName, (Status)filterByStatusDto);

                int totalRecord = listCategoriesTuple.Item2;
                int filterRecord = listCategoriesTuple.Item3;
                // var listCategoriesDto = listCategoriesTuple.Item1.ConvertToDto();
                var listCategoriesDto = listCategoriesTuple.Item1.ConvertToObject();

                // List<object> entitiesList = new List<object>();
                // foreach (var item in listCategoriesDto)
                // {
                //     List<string> dataItems = new List<string>
                //     {
                //         item.Name,
                //         item.StatusHtml,
                //         item.ActionLinkHtml
                //     };

                //     entitiesList.Add(dataItems);
                // }

                // return (entitiesList, totalRecord, filterRecord);
                return (listCategoriesDto, totalRecord, filterRecord);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoryDto> GetCategoryByIdServiceAsync(Guid? entityDtoToGetId)
        {
            try
            {
                var entity = await _unitOfWork.CategoryRepository.GetEntityByIdAsync(entityDtoToGetId);
                if (entity == null) throw new Exception();

                var entityDto = entity.ConvertToDto();
                return entityDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> CreateCategoryServiceAsync(CategoryDto entityDtoToCreate)
        {
            try
            {
                var entity = entityDtoToCreate.ConvertToEntity();

                if (!await _unitOfWork.CategoryRepository.CreateEntityAsync(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateCategoryServiceAsync(CategoryDto entityDtoToUpdate)
        {
            try
            {
                var entity = entityDtoToUpdate.ConvertToEntity();

                entity.ModifiedAt = DateTime.Now;
                entity.ModifiedBy = 4;

                if (!_unitOfWork.CategoryRepository.UpdateEntity(entity)) throw new Exception();

                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCategoryByIdServiceAsync(Guid entityDtoToDeleteId)
        {
            try
            {
                if (!await _unitOfWork.CategoryRepository.DeleteEntityAsync(entityDtoToDeleteId)) return false;
                return await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}