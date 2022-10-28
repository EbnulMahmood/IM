using IM.CoreBusiness.Entities.BaseEntity;
using IM.CoreBusiness.Entities.BaseEntity.Contracts;

namespace IM.CoreBusiness.Entities
{
    public class Category : BaseEntity<Guid>, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}