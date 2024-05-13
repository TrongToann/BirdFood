using BirdFood.Contract.DTOs.AccountDTO;

namespace BirdFood.Contract.DTOs.Auth
{
    public class RegisterDTO : IAccountDTO
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
