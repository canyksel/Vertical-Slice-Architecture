using Example.API.Features.Users;


namespace Example.API.Extensions;

public static class EndpointMappingExtensions
{
    public static void MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        CreateUser.MapEndpoint(app);
        UpdateUser.MapEndpoint(app);
        DeleteUser.MapEndpoint(app);
        GetUser.MapEndpoint(app);
        GetAllUsers.MapEndpoint(app);
    }
}