using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Enumerations;

namespace BirdFood.Contract.Service.Shop
{
    public static class Query
    {
        public record GetShops(
            string? SearchTerm, string? SortColumn,
            Dictionary<string, SortOrder>? SortOrder, int PageIndex, int PageSize) : IQuery<PageResult<Response>>;
        public record FindShop(Guid ShopId) : IQuery<Response>;
    }
}
