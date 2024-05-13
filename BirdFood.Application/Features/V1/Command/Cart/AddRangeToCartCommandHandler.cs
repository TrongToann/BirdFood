using BirdFood.Application.Data;
using BirdFood.Application.Utils;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using static BirdFood.Contract.Abstraction.Shared.ResultExtension;
using static BirdFood.Contract.Service.Cart.Command;

namespace BirdFood.Application.Features.V1.Command.Cart
{
    public class AddRangeToCartCommandHandler : ICommandHandler<AddRangeToCart>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRangeToCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddRangeToCart request, CancellationToken cancellationToken)
        {
            Combine(Result.Create(await _unitOfWork.GetRepository<Domain.Entities.Account, Guid>()
                .FindByIdAsync(request.AddRangeToCartDTO.Account_id)));

            var cart = await CartUtils.FindCartOrInsertIfNotExist(request.AddRangeToCartDTO.Account_id, _unitOfWork);
            
            var newInsert = 0;
            foreach(var product in request.AddRangeToCartDTO.Foods)
            {
                Combine(Result.Create(await _unitOfWork.GetRepository<Domain.Entities.Food, Guid>()
                .FindByIdAsync(product.Food_id)));
                newInsert += await CartUtils.InsertProductToCart(product.Food_id, product.Total, cart.Id, _unitOfWork);
            }
            
            cart.Count_Item += newInsert;
            _unitOfWork.GetRepository<Domain.Entities.Cart, Guid>().Update(cart);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
