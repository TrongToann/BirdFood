using BirdFood.Domain.Abstraction;
using BirdFood.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("CartProducts")]
    public class CartFood : EntityBase<Guid>
    {
        [ForeignKey(nameof(Cart))]
        public Guid Cart_id { get; set; }
        [ForeignKey(nameof(Food))]
        public Guid Food_id { get; set; }
        public int Total { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Food Food { get; set; }
    }
}
