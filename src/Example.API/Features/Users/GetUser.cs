using Example.API.Database;
using Example.API.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.API.Features.Users;

public static class GetUser
{
    public record Query(int Id) : IRequest<Response>;
    public record Response(string FirstName, string LastName, int Age, string Email, Gender Gender);

    internal sealed class Handler(ExampleApplicationDbContext context) : IRequestHandler<Query, Response>
    {
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var userResponse = await context
                .Users
                .Where(u => u.Id == request.Id)
                .Select(u => new Response(u.FirstName, u.LastName, u.Age, u.Email, u.Gender)).FirstOrDefaultAsync(cancellationToken);

            if (userResponse is null)
                throw new Exception("User not found!");

            return userResponse;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/{id}", async (int id, ISender sender) =>
        {
            var user = await sender.Send(new Query(id));

            return Results.Ok(user);
        });
    }
}
