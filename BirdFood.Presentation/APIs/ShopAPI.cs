using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.DTOs.Shop;
using BirdFood.Contract.Enumerations;
using BirdFood.Contract.Extensions;
using BirdFood.Contract.Service;
using BirdFood.Contract.Service.Shop;
using BirdFood.Presentation.Abstractions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static BirdFood.Contract.Service.Shop.Command;
using static BirdFood.Contract.Service.Shop.Query;

namespace BirdFood.Presentation.APIs
{
    public class ShopAPI : ApiEndpoint, ICarterModule
    {
        private const string BaseUrl = "/api/v{version:apiVersion}/shop";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("shops")
               .MapGroup(BaseUrl).HasApiVersion(1);
            group1.MapPost("/", async ([FromBody] CreateShopDTO createShopDTO, ISender sender) =>
            {
                var command = new CreateShop(createShopDTO);
                Result<BaseResponse> repsonse = await sender.Send(command);
                if (repsonse.IsFailure)
                    return HandleFailure(repsonse);
                return Results.Ok(repsonse);
            });
            group1.MapGet("/", async (ISender sender,
                string? searchTerm = null,
                string? sortColumn = null,
                string? sortColumnAndOrder = null,
                int pageIndex = 1,
                int pageSize = 12) =>
            {
                Dictionary<string, SortOrder> sort = SortOrderExtension.CovertStringToSortDictionary(sortColumnAndOrder);
                Result<PageResult<Contract.Service.Shop.Response>> repsonse =
                    await sender.Send(new GetShops(searchTerm, sortColumn, sort, pageIndex, pageSize));
                if (repsonse.IsFailure)
                    return HandleFailure(repsonse);
                return Results.Ok(repsonse);
            });
            group1.MapPut("/{shopId}", async (Guid shopId, [FromBody] UpdateShopDTO updateShopDTO, ISender sender) =>
            {
                Result<BaseResponse> repsonse = await sender.Send(new UpdateShop(shopId, updateShopDTO));
                if (repsonse.IsFailure)
                    return HandleFailure(repsonse);
                return Results.Ok(repsonse);
            });
            group1.MapGet("/detail", async (Guid shopId, ISender sender) =>
            {
                Result<Response> repsonse = await sender.Send(new FindShop(shopId));
                if (repsonse.IsFailure)
                    return HandleFailure(repsonse);
                return Results.Ok(repsonse);
            });

        }
    }
}
