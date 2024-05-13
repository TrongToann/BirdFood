namespace BirdFood.Domain.Abstraction.Entitites
{
    public interface IUserTracking
    {
        string CreatedBy {  get; set; }
        string ModifiedBy { get; set; }
    }
}
