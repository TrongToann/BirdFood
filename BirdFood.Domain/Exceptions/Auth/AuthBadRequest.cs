namespace BirdFood.Domain.Exceptions.Auth
{
    public class AuthBadRequest : BadRequestException
    {
        public AuthBadRequest() : 
            base("Bad Request")
        {
        }
    }
}
