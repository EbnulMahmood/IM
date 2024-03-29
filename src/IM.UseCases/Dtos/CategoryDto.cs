using System.ComponentModel.DataAnnotations;
using IM.UseCases.Dtos.BaseDto;
using IM.UseCases.Dtos.BaseDto.Contracts;

namespace IM.UseCases.Dtos
{
    public class CategoryDto : BaseDto<long>, IDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
    }
}