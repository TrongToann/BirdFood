using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Enumerations;
using BirdFood.Contract.Service.Shop;
using BirdFood.Domain.Abstraction.Repositories;
using System.Linq.Expressions;
using static BirdFood.Contract.Service.Shop.Query;

namespace BirdFood.Application.Features.V1.Queries.Shop
{
    public class GetShopsQueryHandler : IQueryHandler<GetShops, PageResult<Response>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        public GetShopsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IShopRepository shopRepository) =>
            (_unitOfWork, _mapper, _shopRepository) = (unitOfWork, mapper, shopRepository);
        public async Task<Result<PageResult<Response>>> Handle(GetShops request, CancellationToken cancellationToken)
        {
            if(request.SortOrder.Any())
            {
                var pageIndex = request.PageIndex <= 0 ? PageResult<Domain.Entities.Shop>.DefaultPageIndex : request.PageIndex;
                var pageSize = request.PageSize <= 0
                       ? PageResult<Domain.Entities.Shop>.DefaultPageSize
                       : request.PageSize > PageResult<Domain.Entities.Shop>.UpperPageSize
                       ? PageResult<Domain.Entities.Shop>.UpperPageSize : request.PageSize;
                var shopQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                    ? $@"SELECT * FROM {nameof(Domain.Entities.Shop)}s ORDER BY "
                    : $@"SELECT * FROM {nameof(Domain.Entities.Shop)}s 
                        WHERE {nameof(Domain.Entities.Shop.Name)} LIKE '%{request.SearchTerm}%'
                        ORDER BY ";
                    foreach (var item in request.SortOrder)
                        shopQuery += item.Value == SortOrder.Descending
                            ? $"{item.Key} DESC, "
                            : $"{item.Key} ASC, ";
                    shopQuery = shopQuery.Remove(shopQuery.Length - 2);
                shopQuery += $" OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
                var shops = await _shopRepository.RawSQL(shopQuery);
                var totalCount = await _shopRepository.CountTotal();
                var shopPageResult = PageResult<Domain.Entities.Shop>.Create(shops,
                    pageIndex,
                    pageSize,
                    totalCount
                    );
                var result = _mapper.Map<PageResult<Response>>(shopPageResult);
                return Result.Success(result);
            }else
            {
                var shopQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                    ? _shopRepository.GetAll()
                    : _shopRepository.GetAll(x => x.Name.Contains(request.SearchTerm));

                shopQuery = request.SortColumn == SortOrder.Descending
                    ? shopQuery.OrderByDescending(GetSortProperty(request))
                    : shopQuery.OrderBy(GetSortProperty(request));

                var shopPageResult = await PageResult<Domain.Entities.Shop>.CreateAsync(shopQuery,
                    request.PageSize, request.PageSize
                    );
                var result = _mapper.Map<PageResult<Response>>(shopPageResult);
                return Result.Success(result);
            }
        }
        private static Expression<Func<Domain.Entities.Shop, object>> GetSortProperty(GetShops request)
        => request.SortColumn?.ToLower() switch
        {
            "name" => Shop => Shop.Name,
            _ => Shop => Shop.Id
        };
    }
}
