using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("FoodCombo")]
    public class FoodCombo : EntityBase<Guid>
    {
        [ForeignKey(nameof(Combo))]
        public Guid Combo_id { get; set; }
        [ForeignKey(nameof(Food))]
        public Guid Food_id { get; set; }
        public int TotalFood { get; set; }
        public virtual Food Food { get; set; }
        public virtual Combo Combo { get; set; }
    }
}
