using BirdFood.Domain.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdFood.Domain.Entities
{
    [Table("Account")]
    public class Account : EntityBase<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
    }
}
