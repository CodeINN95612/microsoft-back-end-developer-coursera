using SimpleUsersApi.Services;

namespace SimpleUsersApi.Endpoints
{
    public static class DeleteUser
    {
        public static IEndpointRouteBuilder MapDeleteUserEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapDelete("/users/{id:int}", async (IUserService userService, int id) =>
            {
                await Task.CompletedTask;
                if (!userService.Delete(id))
                {
                    return Results.NotFound();
                }
                return Results.Ok();
            })
            .WithName("DeleteUser")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Users");

            return routes;
        }
    }
}
