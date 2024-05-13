namespace BirdFood.Domain.Abstraction.Entitites
{
    public interface IEntityAuditBase<T> : IEntityBase<T>, IAuditable
    {
    }
}
