using IM.CoreBusiness.Enums;
using IM.CoreBusiness.Entities.BaseEntity.Contracts;

namespace IM.CoreBusiness.Entities.BaseEntity
{
    public abstract class BaseEntity<T> : IEntity
    {
        public T Id { get; set; }
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public T CreatedBy { get; set; }
        public T? ModifiedBy { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set {  }
        }

        object IEntity.CreatedBy
        {
            get { return CreatedBy; }
            set { }
        }

        object IEntity.ModifiedBy
        {
            get { return ModifiedBy; }
            set { }
        }
    }
}