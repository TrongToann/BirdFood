namespace BirdFood.Domain.Exceptions.Food
{
    public class FoodBadRequest : BadRequestException
    {
        public FoodBadRequest() :
            base("Bad Request")
        {
        }
    }
}
