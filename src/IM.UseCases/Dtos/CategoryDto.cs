using System.ComponentModel.DataAnnotations;
using IM.UseCases.Dtos.BaseDto;
using IM.UseCases.Dtos.BaseDto.Contracts;

namespace IM.UseCases.Dtos
{
    public class CategoryDto : BaseDto<Guid>, IDto
    {
        [Required]
        [Range(1, 255, ErrorMessage ="The name is out of range")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}