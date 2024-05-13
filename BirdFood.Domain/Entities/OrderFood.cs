using BirdFood.Domain.Abstraction;
using BirdFood.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("OrderFoods")]
    public class OrderFoods : EntityBase<Guid>
    {
        [ForeignKey(nameof(Order))]
        public Guid Order_id { get; set; }
        [ForeignKey(nameof(Food))]
        public Guid Food_id { get; set; }
        public int TotalFood { get; set; }
        public Food Food { get; set; }
        public Order Order { get; set; }
    }
}
