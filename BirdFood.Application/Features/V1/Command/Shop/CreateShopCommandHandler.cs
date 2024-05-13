using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Auth;
using static BirdFood.Contract.Service.Shop.Command;


namespace BirdFood.Application.Features.V1.Command.Shop
{
    public class CreateShopCommandHandler : ICommandHandler<CreateShop, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShopRepository _shopRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CreateShopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IShopRepository shopRepository, IAccountRepository accountRepository) => 
            (_unitOfWork, _mapper, _shopRepository, _accountRepository) = (unitOfWork, mapper, shopRepository, accountRepository);

        public async Task<Result<BaseResponse>> Handle(CreateShop request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var shop = _mapper.Map<Domain.Entities.Shop>(request.CreateShopDTO);
            var userCheck = await _accountRepository.FindByIdAsync(request.CreateShopDTO.Account_id);
            if (userCheck == null) throw new AuthBadRequest();
            shop.Account_id = userCheck.Id;
            _shopRepository.Add(shop);
            await _unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Message = "Create Shop Successfully!";
            response.Errors = [];
            return response;
        }
    }
}
