using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.DTOs.AccountDTO;
using BirdFood.Presentation.Abstractions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static BirdFood.Contract.Service.Account.Command;

namespace BirdFood.Presentation.APIs
{
    public class AccountAPI : ApiEndpoint, ICarterModule
    {
        private const string BaseUrl = "/api/v{version:apiVersion}/account";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("account")
                .MapGroup(BaseUrl).HasApiVersion(1);
            group1.MapPost("/", async ([FromBody] CreateAccountDTO CreateAccountDTO, ISender _sender) =>
            {
                var command = new CreateAccount(CreateAccountDTO);
                Result response = await _sender.Send(command);
                if (response.IsFailure) HandleFailure(response);
                return Results.Ok(response);
            }).RequireAuthorization();
            group1.MapPost("/aa", async ([FromBody] CreateAccountDTO CreateAccountDTO, ISender _sender) =>
            {
                var command = new CreateAccount(CreateAccountDTO);
                Result response = await _sender.Send(command);
                if (response.IsFailure) HandleFailure(response);
                return Results.Ok(response);
            }).RequireAuthorization();
        }
    }
}
