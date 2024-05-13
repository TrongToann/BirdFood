using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Shop;
using static BirdFood.Contract.Service.Food.Command;

namespace BirdFood.Application.Features.V1.Command.Food
{
    public class CreateFoodCommandHandler : ICommandHandler<CreateFood, BaseResponse>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFoodCommandHandler(IFoodRepository foodRepository, IUnitOfWork unitOfWork, 
            IMapper mapper, IShopRepository shopRepository)
        {
            _foodRepository = foodRepository;
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<BaseResponse>> Handle(CreateFood request, CancellationToken cancellationToken)
        {
            if (await _shopRepository.FindByIdAsync(request.CreateFoodDTO.Shop_id) is null)
                throw new ShopNotFound(request.CreateFoodDTO.Shop_id);

            var food = _mapper.Map<Domain.Entities.Food>(request.CreateFoodDTO);
            _foodRepository.Add(food);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse.BuildSuccessResponse(message: "Create Food Successfully!");
        }
    }
}
