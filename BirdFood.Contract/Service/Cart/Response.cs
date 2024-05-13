using BirdFood.Contract.DTOs.AccountDTO;
using BirdFood.Contract.DTOs.CartDTO;
using System.Collections.ObjectModel;

namespace BirdFood.Contract.Service.Cart
{
    public class Response : ICartDTO
    {
        public Guid Account_id { get; set; }
        public int Count_Item { get; set; }
        public AccountDTO Account { get; set; }
        public Collection<CartFoodDTO> CartFoods { get; set; }
        
    }
}
