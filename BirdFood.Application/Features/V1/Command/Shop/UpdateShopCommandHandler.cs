using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Shop;
using static BirdFood.Contract.Service.Shop.Command;

namespace BirdFood.Application.Features.V1.Command.Shop
{
    public class UpdateShopCommandHandler : ICommandHandler<UpdateShop, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;

        public UpdateShopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IShopRepository shopRepository)
        => (_unitOfWork, _mapper, _shopRepository) = (unitOfWork, mapper, shopRepository);

        public async Task<Result<BaseResponse>> Handle(UpdateShop request, CancellationToken cancellationToken)
        {
            var shopCheck = await _shopRepository.FindByIdAsync(request.ShopId);
            if (shopCheck == null) throw new ShopNotFound(request.ShopId);
            shopCheck.Name = request.UpdateShopDTO.Name;
            shopCheck.Avatar = request.UpdateShopDTO.Avatar;
            shopCheck.Description = request.UpdateShopDTO.Description;
            _shopRepository.Update(shopCheck);
            await _unitOfWork.SaveChangesAsync();
            var response = new BaseResponse();
            response.Success = true;
            response.Message = "Update Shop Successfully!";
            response.Errors = [];
            return response;
        }
    }
}
