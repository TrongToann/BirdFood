using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("Food")]
    public class Food : EntityAuditBase<Guid>
    {
        [ForeignKey(nameof(Shop))]
        public Guid Shop_id { get; set; }
        public string Name { get; set; }
        public string Thumb { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int In_stock { get; set; }
        public virtual Shop Shop { get; set; }
        public ICollection<FoodCombo> FoodCombos { get; set; }
    }
}
