using IM.UseCases.Dtos.BaseDto.Contracts;
using IM.UseCases.Dtos.Enums;

namespace IM.UseCases.Dtos.BaseDto
{
    public class BaseDto<T> : IDto
    {
        public T Id { get; set; }
        public StatusDto Status { get; set; } = StatusDto.Active;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
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