using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("Shops")]
    public class Shop : EntityBase<Guid>
    {
        [ForeignKey(nameof(Account))]
        public Guid Account_id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
