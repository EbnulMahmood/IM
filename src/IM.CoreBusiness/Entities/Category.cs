using IM.CoreBusiness.Entities.BaseEntity;
using IM.CoreBusiness.Entities.BaseEntity.Contracts;

namespace IM.CoreBusiness.Entities
{
    public class Category : BaseEntity<long>, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public ICollection<Product>? Products { get; set; }
    }
}