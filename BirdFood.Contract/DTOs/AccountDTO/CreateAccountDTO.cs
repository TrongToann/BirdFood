namespace BirdFood.Contract.DTOs.AccountDTO
{
    public class CreateAccountDTO : IAccountDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Role {  get; set; }
        public int Status { get; set; }
    }
}
