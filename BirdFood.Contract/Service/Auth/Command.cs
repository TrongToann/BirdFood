using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.DTOs.Auth;
using BirdFood.Contract.DTOs.BaseDTO;

namespace BirdFood.Contract.Service.Auth
{
    public static class Command
    {
        public record Login(LoginDTO LoginDTO) : ICommand<Response> { }
        public record Register(RegisterDTO RegisterDTO) : ICommand<BaseResponse> { }
        public record Logout(Guid Account_id) : ICommand<BaseResponse> { }
    }
}
