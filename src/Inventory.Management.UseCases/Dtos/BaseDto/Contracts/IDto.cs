using Inventory.Management.CoreBusiness.Enums;

namespace Inventory.Management.UseCases.Dtos.BaseDto.Contracts
{
    public interface IDto
    {
        object Id { get; set; }
        Status Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        object CreatedBy { get; set; }
        object? ModifiedBy { get; set; }
    }
}