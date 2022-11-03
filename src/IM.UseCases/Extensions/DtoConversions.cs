using IM.CoreBusiness.Entities;
using IM.CoreBusiness.Enums;
using IM.UseCases.Dtos;
using IM.UseCases.Dtos.Enums;

namespace IM.UseCases.Extensions
{
    public static class DtoConversions
    {
        private static CategoryDto NewCategoryDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = (StatusDto)category.Status,
                CreatedAt = category.CreatedAt,
                ModifiedAt = category.ModifiedAt,
                CreatedBy = category.CreatedBy,
                ModifiedBy = category.ModifiedBy,
            };
        }

        private static Category NewCategory(CategoryDto categoryDto)
        {
            return new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                Status = (Status)categoryDto.Status,
                CreatedAt = categoryDto.CreatedAt,
                ModifiedAt = categoryDto.ModifiedAt,
                CreatedBy = categoryDto.CreatedBy,
                ModifiedBy = categoryDto.ModifiedBy,
            };
        }

        public static IEnumerable<CategoryDto> ConvertToDto(this IEnumerable<Category> categories)
        {
            return (from category in categories
                    select new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Status = (StatusDto)category.Status,
            }).ToList();
        }

        public static CategoryDto ConvertToDto(this Category category)
        {
            return NewCategoryDto(category);
        }

        public static Category ConvertToEntity(this CategoryDto categoryDto)
        {
            return NewCategory(categoryDto);
        }
    }
}