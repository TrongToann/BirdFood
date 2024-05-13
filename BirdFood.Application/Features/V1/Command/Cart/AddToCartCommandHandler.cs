using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service;
using BirdFood.Domain.Exceptions.Common;
using BirdFood.Domain.Exceptions.User;
using static BirdFood.Contract.Service.Cart.Command;

namespace BirdFood.Application.Features.V1.Command.Cart
{
    public class AddToCartCommandHandler : ICommandHandler<AddToCart, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddToCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) => 
              (_unitOfWork, _mapper) = (unitOfWork, mapper);
        public async Task<Result<BaseResponse>> Handle(AddToCart request, CancellationToken cancellationToken)
        {
            await CheckValidUser(request.addToCart.Account_id);
            var cartCheck = await FindCartOrInsertIfNotExist(request);
            await InsertProductToCart(request, cartCheck);
            cartCheck.Count_Item += 1;
             _unitOfWork.GetRepository<Domain.Entities.Cart, Guid>().Update(cartCheck);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse.BuildSuccessResponse(message: "Add Successfully!");
        }
        private async Task InsertProductToCart(AddToCart request, Domain.Entities.Cart cart)
        {
            var attributeCheck = await _unitOfWork.GetRepository<Domain.Entities.CartFood, Guid>()
                .FindSingleAsync(x => x.Food_id == request.addToCart.InputFoodDTO.Food_id);

            if (attributeCheck != null)
            {
                attributeCheck.Total += request.addToCart.InputFoodDTO.Total;
                _unitOfWork.GetRepository<Domain.Entities.CartFood, Guid>().Update(attributeCheck);
                return;
            }
            var attribute = new Domain.Entities.CartFood
            {
                 Cart_id = cart.Id,
                 Food_id = request.addToCart.InputFoodDTO.Food_id,
                 Total = request.addToCart.InputFoodDTO.Total,
              };
             _unitOfWork.GetRepository<Domain.Entities.CartFood, Guid>().Add(attribute);
            return;
        }
        private async Task CheckValidUser(Guid User_id)
        {
            var userCheck = await _unitOfWork.GetRepository<Domain.Entities.Account, Guid>()
                .FindByIdAsync(User_id);
            if (userCheck == null) throw new AccountNotFound(User_id);
            return;
        }
        private async Task<Domain.Entities.Cart> FindCartOrInsertIfNotExist(AddToCart request)
        {
            var cartCheck = await _unitOfWork.GetRepository<Domain.Entities.Cart, Guid>()
                .FindSingleAsync(x => x.Account_id == request.addToCart.Account_id);
            if (cartCheck == null)
            {
                var cart = _mapper.Map<Domain.Entities.Cart>(request.addToCart);
                cart.Count_Item = 0;
                _unitOfWork.GetRepository<Domain.Entities.Cart, Guid>().Add(cart);
                var affectedRecords = await _unitOfWork.SaveChangesAsync();
                if (affectedRecords < 1) throw new InternalServerError();
                cartCheck = cart;
            }
            return cartCheck;
        }
    }
    
}
