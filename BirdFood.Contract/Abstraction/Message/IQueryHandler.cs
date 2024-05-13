using BirdFood.Contract.Abstraction.Shared;
using MediatR;

namespace BirdFood.Contract.Abstraction.Message
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
