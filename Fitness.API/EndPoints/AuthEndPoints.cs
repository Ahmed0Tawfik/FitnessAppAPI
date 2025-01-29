using Fitness.Application;
using Fitness.Application.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.EndPoints
{
    public static class AuthEndPoints
    {
        public static void MapAuthEndPoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/auth")
                .WithOpenApi();

            endpoints.MapPost("/register", async (
                [FromBody] UserRegisterCommand request,
                [FromServices] IRequestHandler<UserRegisterCommand, UserResponse> handler,
                IValidator<UserRegisterCommand> validator,
                CancellationToken ct) =>
            {
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Register User");


            endpoints.MapPost("/login", async (
                [FromBody] UserLoginCommand request,
                [FromServices] IRequestHandler<UserLoginCommand, UserResponse> handler,
                IValidator<UserLoginCommand> validator,
                CancellationToken ct) =>
            {
                await validator.ValidateAndThrowAsync(request, ct);
                return await handler.Handle(request, ct);
            })
            .WithSummary("Login User");
            



        }
    }
}
