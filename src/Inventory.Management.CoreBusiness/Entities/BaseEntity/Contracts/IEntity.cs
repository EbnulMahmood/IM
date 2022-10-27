using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Management.CoreBusiness.Entities.BaseEntity.Contracts
{
    public interface IEntity
    {
        object Id { get; set; }
        Status Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        object CreatedBy { get; set; }
        object ModifiedBy { get; set; }
    }
}