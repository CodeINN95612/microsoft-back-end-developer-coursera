using SimpleUsersApi.Models;
using SimpleUsersApi.Services;
using SimpleUsersApi.Validators;

namespace SimpleUsersApi.Endpoints
{
    public static class CreateUser
    {
        public record CreateUserRequest(string Name, string Email, string Password);

        public record CreateUserResponse(int Id, string Name, string Email);

        public static IEndpointRouteBuilder MapCreateUserEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/users", async (IUserService userService, CreateUserRequest request) =>
            {
                await Task.CompletedTask;

                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return Results.BadRequest("Name, Email, and Password are required.");
                }

                if (!EmailValidator.IsValidEmail(request.Email))
                {
                    return Results.BadRequest("Email Invalid");
                }

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = request.Password
                };
                var createdUser = userService.Create(user);
                return Results.Created($"/users/{createdUser.Id}", new CreateUserResponse(createdUser.Id, createdUser.Name, createdUser.Email));
            })
            .WithName("CreateUser")
            .Produces<CreateUserResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Users");

            return routes;
        }
    }
}
