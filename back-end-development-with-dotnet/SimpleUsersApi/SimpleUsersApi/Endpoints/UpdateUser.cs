using SimpleUsersApi.Models;
using SimpleUsersApi.Services;
using SimpleUsersApi.Validators;

namespace SimpleUsersApi.Endpoints
{
    public static class UpdateUser
    {
        public record UpdateUserRequest(string Name, string Email, string Password);
        public static IEndpointRouteBuilder MapUpdateUserEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPut("/users/{id:int}", async (IUserService userService, int id, UpdateUserRequest request) =>
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
                if (!userService.Update(id, user))
                {
                    return Results.NotFound();
                }
                return Results.Ok();
            })
            .WithName("UpdateUser")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Users");

            return routes;
        }
    }
}
