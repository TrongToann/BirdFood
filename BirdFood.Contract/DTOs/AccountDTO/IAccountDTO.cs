namespace BirdFood.Contract.DTOs.AccountDTO
{
    public interface IAccountDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
