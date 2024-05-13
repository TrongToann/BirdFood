
namespace BirdFood.Domain.Exceptions.Auth
{
    public class AuthNotFound : NotFoundException
    {
        public AuthNotFound(string username) : 
            base($"Account with username {username} was not found")
        {
        }
    }
}
