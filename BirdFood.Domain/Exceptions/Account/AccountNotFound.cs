namespace BirdFood.Domain.Exceptions.User
{
    public class AccountNotFound : NotFoundException
    {
        public AccountNotFound(string username) : 
            base($"User not found with username {username}" )
        {
        }
        public AccountNotFound(Guid username) :
            base($"User not found with id {username}")
        {
        }
    }
}
