using IM.UseCases.Dtos.Enums;

namespace IM.UseCases.Dtos.BaseDto.Contracts
{
    public interface IDto
    {
        object Id { get; set; }
        StatusDto Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        object CreatedBy { get; set; }
        object? ModifiedBy { get; set; }
    }
}