using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("Carts")]
    public class Cart : EntityBase<Guid>
    {
        [ForeignKey(nameof(Account))]
        public Guid Account_id { get; set; }
        public virtual Account Account { get; set; }
        public int Count_Item { get; set; }
        public virtual ICollection<CartFood> CartFoods { get; set; }
    }
}
