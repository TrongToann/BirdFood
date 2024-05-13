using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.DTOs.FoodDTO;

namespace BirdFood.Contract.Service.Food
{
    public static class Command
    {
        public record CreateFood(CreateFoodDTO CreateFoodDTO) : ICommand<BaseResponse>;
    }
}
