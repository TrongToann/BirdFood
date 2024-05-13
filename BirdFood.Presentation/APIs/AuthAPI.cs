using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.DTOs.AccountDTO;
using BirdFood.Contract.DTOs.Auth;
using BirdFood.Contract.Service;
using BirdFood.Contract.Service.Auth;
using BirdFood.Presentation.Abstractions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static BirdFood.Contract.Service.Auth.Command;

namespace BirdFood.Presentation.APIs
{
    public class AuthAPI : ApiEndpoint, ICarterModule
    {
        private const string BaseUrl = "/api/v{version:apiVersion}/auth";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("auth")
                .MapGroup(BaseUrl).HasApiVersion(1);
            group1.MapPost("/login", async ([FromBody] LoginDTO LoginDTO, ISender _sender) =>
            {
                var command = new Login(LoginDTO);
                Result<Response> response = await _sender.Send(command);
                if (response.IsFailure)
                    HandleFailure(response);
                return Results.Ok(response);
            });
            group1.MapGet("/register", async ([FromBody] RegisterDTO RegisterDTO, ISender _sender) =>
            {
                var command = new Register(RegisterDTO);
                Result<BaseResponse> response = await _sender.Send(command);
                if (response.IsFailure) HandleFailure(response);
                return Results.Ok(response);
            });
            group1.MapGet("/logout", async ([FromBody] RegisterDTO RegisterDTO, ISender _sender) =>
            {
                var command = new Register(RegisterDTO);
                Result<BaseResponse> response = await _sender.Send(command);
                if (response.IsFailure) HandleFailure(response);
                return Results.Ok(response);
            });
        }
    }
}
