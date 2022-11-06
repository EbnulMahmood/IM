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

        private static string ConditionClassStatus(StatusDto statusDto)
        {
            return statusDto == StatusDto.Active ? "success" : "warning";
        }

        private static string ConditionTextStatus(StatusDto statusDto)
        {
            return statusDto == StatusDto.Active ? "Active" : "Inactive";
        }

        private static string ActionLinks(long id)
        {
            return $"<div class='btn-group' role='group'>" +
                        $"<a href='Edit/{id}' class='btn btn-primary mx-1'><i class='bi bi-pencil-square'></i>Edit</a>" +
                        $"<button type='button' data-bs-target='#deleteCategory' data-bs-toggle='ajax-modal'" +
                            $"class='btn btn-danger mx-1 btn-category-delete' data-category-id='{id}'>" +
                            $"<i class='bi bi-trash-fill'></i>Delete</button>" +
                        $"<a href='Details/{id}' class='btn btn-info mx-1'><i class='bi bi-ticket-detailed-fill'>" +
                            $"</i>Details</a>" +
                    $"</div>";
        }

        public static IEnumerable<CategoryDto> ConvertToDto(this IEnumerable<Category> categories)
        {
            return (from category in categories
                    select new CategoryDto
            {
                Name = category.Name,
                StatusHtml = $"<span class='badge alert-{ConditionClassStatus((StatusDto)category.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)category.Status)}</span>",
                ActionLinkHtml = ActionLinks(category.Id),
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

        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
        {
            return (from product in products
                    select new ProductDto
            {
                Name = product.Name,
                CategoryName = "product.Category.Name",
                StatusHtml = $"<span class='badge alert-{ConditionClassStatus((StatusDto)product.Status)}'>" +
                             $"{ConditionTextStatus((StatusDto)product.Status)}</span>",
                ActionLinkHtml = ActionLinks(product.Id),
            }).ToList();
        }

        public static Product ConvertToEntity(this ProductDto productDto)
        {
            return new Product()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
            };
        }
    }
}