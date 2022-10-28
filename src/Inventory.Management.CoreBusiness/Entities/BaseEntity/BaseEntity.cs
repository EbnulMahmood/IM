using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Management.CoreBusiness.Enums;
using Inventory.Management.CoreBusiness.Entities.BaseEntity.Contracts;

namespace Inventory.Management.CoreBusiness.Entities.BaseEntity
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