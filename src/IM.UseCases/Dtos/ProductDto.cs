using System.ComponentModel.DataAnnotations;
using IM.UseCases.Dtos.BaseDto;
using IM.UseCases.Dtos.BaseDto.Contracts;

namespace IM.UseCases.Dtos
{
    public class ProductDto : BaseDto<long>, IDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}