using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("OrderStatus")]
    public class OrderStatus : EntityBase<Guid>
    {
        [ForeignKey(nameof(Order))]
        public Guid Order_id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Date {  get; set; }
        public virtual Order Order { get; set; }
    }
}
