using SimpleUsersApi.Models;
using SimpleUsersApi.Services;

namespace SimpleUsersApi.Endpoints
{
    public static class GetAllUsers
    {
        public static IEndpointRouteBuilder MapGetAllUsersEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/users", async (IUserService userService) =>
            {
                await Task.CompletedTask;
                var users = userService.GetAll();
                return Results.Ok(users);
            })
            .WithName("GetAllUsers")
            .Produces<List<User>>(StatusCodes.Status200OK)
            .WithTags("Users");

            return routes;
        }
    }
}
