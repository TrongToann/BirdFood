using BirdFood.Contract.Abstraction.Shared;
using MediatR;

namespace BirdFood.Contract.Abstraction.Message
{
    public interface IQuery<TRespone> : IRequest<Result<TRespone>> { }
}
