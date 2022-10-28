using System.ComponentModel.DataAnnotations;
using Inventory.Management.CoreBusiness.Enums;
using Inventory.Management.UseCases.Dtos.BaseDto.Contracts;

namespace Inventory.Management.UseCases.Dtos.BaseDto
{
    public class BaseDto<T> : IDto
    {
        [Required]
        public T Id { get; set; }
        [Required]
        public Status Status { get; set; } = Status.Active;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        [Required]
        public T CreatedBy { get; set; }
        public T? ModifiedBy { get; set; }

        object IDto.Id
        {
            get { return Id; }
            set {  }
        }

        object IDto.CreatedBy
        {
            get { return CreatedBy; }
            set { }
        }

        object IDto.ModifiedBy
        {
            get { return ModifiedBy; }
            set { }
        }
    }
}