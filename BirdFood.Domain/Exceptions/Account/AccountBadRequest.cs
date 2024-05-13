namespace BirdFood.Domain.Exceptions.User
{
    public class AccountBadRequest : BadRequestException
    {
        public AccountBadRequest() :
            base("Bad Request")
        {
        }
    }
}
