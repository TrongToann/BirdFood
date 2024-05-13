using AutoMapper;
using BirdFood.Application.Abstractions;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service.Order;
using Microsoft.EntityFrameworkCore;
using static BirdFood.Contract.Service.Order.Query;

namespace BirdFood.Application.Features.V1.Queries.Order
{
    public class FindOrderByIdQueryHandler : IQueryHandler<FindOrderById, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FindOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<Result<Response>> Handle(FindOrderById request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetRepository<Domain.Entities.Order, Guid>()
                .FindByIdAsync(request.Order_id,
                    includeProperties: [x => x.OrderFoods, x => x.OrderShipping, x => x.OrderStatus]);
            order.OrderFoods = await _unitOfWork.GetRepository<Domain.Entities.OrderFoods, Guid>()
                .GetAll(x => x.Order_id == order.Id, includeProperties: x => x.Food)
                .ToListAsync(cancellationToken: cancellationToken);
            order.OrderStatus = await _unitOfWork.GetRepository<Domain.Entities.OrderStatus, Guid>()
                .GetAll(x => x.Order_id == order.Id)
                .ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<Response>(order);
        }
    }
}
