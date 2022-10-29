using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IM.UseCases.Dtos;
using IM.UseCases.Extensions;
using IM.UseCases.PluginInterfaces;

namespace IM.UseCases.Services
{
    public class CategoryService
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
    }
}