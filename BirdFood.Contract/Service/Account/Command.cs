using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.DTOs.AccountDTO;

namespace BirdFood.Contract.Service.Account
{
    public static class Command
    {
        public record CreateAccount(CreateAccountDTO CreateAccountDTO) : ICommand { }
    }
}
