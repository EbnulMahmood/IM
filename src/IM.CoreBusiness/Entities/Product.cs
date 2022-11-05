using IM.CoreBusiness.Entities.BaseEntity;
using IM.CoreBusiness.Entities.BaseEntity.Contracts;

namespace IM.CoreBusiness.Entities
{
    public class Product : BaseEntity<long>, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Limited { get; set; }
        public int InStock { get; set; }
        public decimal PricePerUnit { get; set; }
        public string BasicUnit { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}