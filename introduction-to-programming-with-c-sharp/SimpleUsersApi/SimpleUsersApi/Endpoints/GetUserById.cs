using SimpleUsersApi.Models;
using SimpleUsersApi.Services;

namespace SimpleUsersApi.Endpoints
{
    public static class GetUserById
    {
        public static IEndpointRouteBuilder MapGetUserByIdEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/users/{id:int}", async (IUserService userService, int id) =>
            {
                await Task.CompletedTask;
                var user = userService.GetById(id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(user);
            })
            .WithName("GetUserById")
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Users");

            return routes;
        }
    }
}
