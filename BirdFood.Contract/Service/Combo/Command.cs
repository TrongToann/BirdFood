using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.DTOs.ComboDTO;

namespace BirdFood.Contract.Service.Combo
{
    public static class Command
    {
        public record CreateCombo(CreateComboDTO CreateComboDTO) : ICommand<BaseResponse> { }
    }
}
