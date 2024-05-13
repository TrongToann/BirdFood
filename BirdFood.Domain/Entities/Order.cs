using BirdFood.Domain.Abstraction;
using BirdFood.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("Orders")]
    public class Order : EntityBase<Guid>
    {
        [Column]
        [Required]
        [ForeignKey(nameof(Account))]
        public Guid Account_id { get; set; }
        public decimal TotalOrder { get; set; }
        public string Payment_Medthod { get; set; }
        public virtual Account Account { get; set; }
        public virtual OrderShipping OrderShipping { get; set; }
        public virtual ICollection<OrderFoods> OrderFoods {  get; set;  }
        public virtual ICollection<OrderStatus> OrderStatus { get; set; }

    }
}
