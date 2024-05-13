namespace BirdFood.Domain.Abstraction.Entitites
{
    public interface IDateTracking
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
