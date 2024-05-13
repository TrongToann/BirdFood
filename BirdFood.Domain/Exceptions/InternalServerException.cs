namespace BirdFood.Domain.Exceptions
{
    public abstract class InternalServerException : DomainException
    {
        protected InternalServerException(string message) : base("Internal Server Error", message)
        {
        }
    }
}
