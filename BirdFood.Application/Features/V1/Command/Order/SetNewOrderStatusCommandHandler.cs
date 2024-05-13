using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service;
using static BirdFood.Contract.Service.Order.Command;

namespace BirdFood.Application.Features.V1.Command.Order
{
    public class SetNewOrderStatusCommandHandler : ICommandHandler<SetNewOrderStatus, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetNewOrderStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<Result<BaseResponse>> Handle(SetNewOrderStatus request, CancellationToken cancellationToken)
        {
            var orderStatus = _mapper.Map<Domain.Entities.OrderStatus>(request.OrderStatus);
            _unitOfWork.GetRepository<Domain.Entities.OrderStatus, Guid>()
                .Add(orderStatus);
            await _unitOfWork.SaveChangesAsync();
            return BaseResponse.BuildSuccessResponse("Update Order Successfully!");
            
        }
    }
}
