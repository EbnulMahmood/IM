using System.ComponentModel.DataAnnotations;
using Inventory.Management.UseCases.Dtos.BaseDto;
using Inventory.Management.UseCases.Dtos.BaseDto.Contracts;

namespace Inventory.Management.UseCases.Dtos
{
    public class CategoryDto : BaseDto<Guid>, IDto
    {
        [Required]
        [Range(0, 255, ErrorMessage ="The name is out of range")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}