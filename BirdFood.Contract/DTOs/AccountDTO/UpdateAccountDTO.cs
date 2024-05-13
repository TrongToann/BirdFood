using BirdFood.Contract.DTOs.BaseDTO;

namespace BirdFood.Contract.DTOs.AccountDTO
{
    public class UpdateAccountDTO : Base, IAccountDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
    }
}
