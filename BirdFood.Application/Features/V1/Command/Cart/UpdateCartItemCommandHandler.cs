using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service.Cart;
using BirdFood.Domain.Exceptions.Cart;
using Microsoft.EntityFrameworkCore;
using static BirdFood.Contract.Service.Cart.Command;

namespace BirdFood.Application.Features.V1.Command.Cart
{
    public class UpdateCartItemCommandHandler : ICommandHandler<UpdateCartItem, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);
        public async Task<Result<Response>> Handle(UpdateCartItem request, CancellationToken cancellationToken)
        {
            var Cart = await _unitOfWork.GetRepository<Domain.Entities.Cart, Guid>()
                .FindByIdAsync(request.Cart_id);
            if (Cart == null) throw new CartNotFound(request.Cart_id);
            var CartProduct = await _unitOfWork.GetRepository<Domain.Entities.CartFood, Guid>()
                 .FindSingleAsync(x => x.Cart_id == request.Cart_id && x.Food_id == request.UpdateCartItemDTO.Food_id);
            if (CartProduct == null) throw new CartNotFound(request.Cart_id);

            CartProduct.Total = request.UpdateCartItemDTO.Type.ToLower().Equals("increase")
                ? CartProduct.Total += request.UpdateCartItemDTO.Quantity
                : CartProduct.Total -= request.UpdateCartItemDTO.Quantity;

            Cart.Count_Item = request.UpdateCartItemDTO.Type.ToLower().Equals("increase")
                ? Cart.Count_Item += request.UpdateCartItemDTO.Quantity
                : Cart.Count_Item -= request.UpdateCartItemDTO.Quantity;

            _unitOfWork.GetRepository<Domain.Entities.Cart, Guid>().Update(Cart);
            _unitOfWork.GetRepository<Domain.Entities.CartFood, Guid>().Update(CartProduct);
            await _unitOfWork.SaveChangesAsync();

            var CartFoods = await _unitOfWork.GetRepository<Domain.Entities.CartFood, Guid>()
                 .GetAll(x => x.Cart_id == request.Cart_id, includeProperties: x => x.Food)
                 .ToListAsync(cancellationToken: cancellationToken);
            Cart.CartFoods = CartFoods;
            
            return _mapper.Map<Response>(Cart);
        }
    }
}
