using Scalar.AspNetCore;
using SimpleUsersApi.Endpoints;
using SimpleUsersApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddHttpLogging();

builder.Services.AddProblemDetails();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


// Add this middleware before other middlewares
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);
        await next();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An unhandled exception occurred while processing the request.");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("An internal server error occurred.");
    }
});

app.UseHttpLogging();
app.UseHttpsRedirection();

app
    .MapGetAllUsersEndpoints()
    .MapGetUserByIdEndpoints()
    .MapCreateUserEndpoints()
    .MapUpdateUserEndpoints()
    .MapDeleteUserEndpoints();

app.Run();
