using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service.Shop;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Shop;
using System.Linq.Expressions;
using static BirdFood.Contract.Service.Shop.Query;

namespace BirdFood.Application.Features.V1.Queries.Shop
{
    public class FindShopQueryHandler : IQueryHandler<FindShop, Response>
    {
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;

        public FindShopQueryHandler(IMapper mapper, IShopRepository shopRepository) =>
            (_mapper, _shopRepository) = (mapper, shopRepository);

        public async Task<Result<Response>> Handle(FindShop request, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Shop, bool>> predicate = x => x.Id == request.ShopId;
            Expression<Func<Domain.Entities.Shop, object>>[] includeProperties = {
                 x => x.Account
            };
            var shop = await _shopRepository
                .FindSingleAsync(predicate, includeProperties: s => s.Account);
            if (shop == null) throw new ShopNotFound(request.ShopId);
            var response = _mapper.Map<Response>(shop);
            return response;
        }

    }
}
