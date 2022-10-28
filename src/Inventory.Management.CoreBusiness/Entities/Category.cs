using Inventory.Management.CoreBusiness.Entities.BaseEntity;
using Inventory.Management.CoreBusiness.Entities.BaseEntity.Contracts;

namespace Inventory.Management.CoreBusiness.Entities
{
    public class Category : BaseEntity<Guid>, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}