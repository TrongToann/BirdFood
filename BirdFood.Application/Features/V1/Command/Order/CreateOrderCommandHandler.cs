using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.DTOs.OrderDTO;
using BirdFood.Contract.Enumerations;
using BirdFood.Contract.Service;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Food;
using static BirdFood.Contract.Service.Order.Command;

namespace BirdFood.Application.Features.V1.Command.Order
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrder, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IFoodRepository foodRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        public async Task<Result<BaseResponse>> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Domain.Entities.Order>(request.CreateOrderDTO);
            decimal totalOrder = await CalculateTotalOrder(request.CreateOrderDTO.OrderFoods);
            order.TotalOrder = totalOrder;

            await SaveOrderDetails(order, request.CreateOrderDTO);

            return BaseResponse.BuildSuccessResponse("Create Order Successfully!");
        }

        private async Task<decimal> CalculateTotalOrder(IEnumerable<InputOrderFood> orderFoods)
        {
            decimal total = 0;
            foreach (var item in orderFoods)
            {
                var food = await _foodRepository.CheckFoodExist(item.Food_id);
                if (food.In_stock < item.TotalFood)
                    throw new FoodBadRequest();

                food.In_stock -= item.TotalFood;
                total += item.TotalFood * food.Price;

                _unitOfWork.GetRepository<Domain.Entities.Food, Guid>().Update(food);
            }
            return total;
        }

        private async Task SaveOrderDetails(Domain.Entities.Order order, CreateOrderDTO createOrderDTO)
        {
            _unitOfWork.GetRepository<Domain.Entities.Order, Guid>().Add(order);
            await _unitOfWork.SaveChangesAsync();

            foreach (var item in createOrderDTO.OrderFoods)
            {
                _unitOfWork.GetRepository<Domain.Entities.OrderFoods, Guid>()
                    .Add(new Domain.Entities.OrderFoods
                        {
                            Order_id = order.Id,
                            Food_id = item.Food_id,
                            TotalFood = item.TotalFood,
                        });
            }

            _unitOfWork.GetRepository<Domain.Entities.OrderShipping, Guid>()
                .Add(new Domain.Entities.OrderShipping
                    {
                        Order_id = order.Id,
                        Province = createOrderDTO.OrderShipping.Province,
                        District = createOrderDTO.OrderShipping.District,
                        Address = createOrderDTO.OrderShipping.Address,
                    });

            _unitOfWork.GetRepository<Domain.Entities.OrderStatus, Guid>()
                .Add(new Domain.Entities.OrderStatus
                    {
                        Order_id = order.Id,
                        Name = OrderStatusType.PENDING,
                        Note = "Waiting for ShopShop Checking!",
                        Date = DateTime.Now.ToString(),
                    });

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
