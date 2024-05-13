using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Domain.Entities;

namespace BirdFood.Application.Utils
{
    public static class CartUtils
    {
        public static async Task<int> InsertProductToCart(Guid Food_id, int Total, Guid Cart_id, IUnitOfWork _unitOfWork)
        {
            var attributeCheck = await _unitOfWork.GetRepository<CartFood, Guid>()
                .FindSingleAsync(x => x.Food_id == Food_id);

            if (attributeCheck is not null)
            {
                attributeCheck.Total += Total;
                _unitOfWork.GetRepository<CartFood, Guid>().Update(attributeCheck);
                return 0;
            }
            _unitOfWork.GetRepository<CartFood, Guid>().Add(new CartFood
            {
                Cart_id = Cart_id,
                Food_id = Food_id,
                Total = Total,
            });
            return 1;
        }
        public static async Task<Cart> FindCartOrInsertIfNotExist(Guid Account_id, IUnitOfWork unitOfWork)
        {
            var cartCheck = await unitOfWork.GetRepository<Cart, Guid>()
              .FindSingleAsync(x => x.Account_id == Account_id);

            return cartCheck ?? await CreateNewCart(Account_id, unitOfWork);
        }

        private static async Task<Cart> CreateNewCart(Guid Account_id, IUnitOfWork unitOfWork)
        {
            var cart = new Cart
            {
                Account_id = Account_id,
                Count_Item = 0 
            };

            unitOfWork.GetRepository<Cart, Guid>().Add(cart);
            await unitOfWork.SaveChangesAsync();
            return cart;
        }
    }
}
