using BirdFood.Contract.Abstraction.Shared;
using MediatR;

namespace BirdFood.Contract.Abstraction.Message
{
    public interface ICommand : IRequest<Result> { }
    public interface ICommand<TRespone> : IRequest<Result<TRespone>> { }
}
