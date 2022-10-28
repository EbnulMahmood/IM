using Inventory.Management.UseCases.Dtos.Enums;

namespace Inventory.Management.UseCases.Dtos.BaseDto.Contracts
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