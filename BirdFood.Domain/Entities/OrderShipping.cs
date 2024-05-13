using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("OrderShippings")]
    public class OrderShipping : EntityBase<Guid>
    {
        [ForeignKey(nameof(Order))]
        public Guid Order_id { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public virtual Order Order { get; set; }
    }
}
