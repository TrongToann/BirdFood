using BirdFood.Domain.Abstraction.Entitites;

namespace BirdFood.Domain.Abstraction
{
    public abstract class EntityBase<T> : IEntityBase<T>
    {
        public T Id { get; set; }
    }
}
