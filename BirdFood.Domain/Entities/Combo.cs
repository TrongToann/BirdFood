using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("Combo")]
    public class Combo : EntityAuditBase<Guid>
    {
        public string Name { get; set; }
        public string Thumb { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<FoodCombo> FoodCombos { get; set; }
    }
}
