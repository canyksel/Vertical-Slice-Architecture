using Example.API.Database;
using Example.API.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.API.Features.Users;

public static class GetAllUsers
{
    public record Query : IRequest<IEnumerable<Response>>;

    public record Response(string FirstName, string LastName, int Age, string Email, Gender Gender);

    internal sealed class Handler(ExampleApplicationDbContext context) : IRequestHandler<Query, IEnumerable<Response>>
    {
        public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var users = await context.Users.ToListAsync(cancellationToken);

            return users.Select(u => new Response(u.FirstName, u.LastName, u.Age, u.Email, u.Gender));
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/users", async ([AsParameters] Query query, ISender sender) =>
        {
            var users = await sender.Send(query);

            return Results.Ok(users);
        });
    }
}