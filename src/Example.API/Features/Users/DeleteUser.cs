using Example.API.Database;
using MediatR;

namespace Example.API.Features.Users;

public static class DeleteUser
{
    public class Command : IRequest<bool>
    {
        public int Id { get; set; }
    }

    internal sealed class Handler(ExampleApplicationDbContext context) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(request.Id);

            if (user is null) throw new Exception("User not found!");

            user.DeleteUser();
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("api/user{id}", async (int id, ISender sender) =>
        {
            var isUserDeleted = await sender.Send(new Command() { Id = id });
            
            return isUserDeleted  ? Results.NoContent() : Results.NotFound($"User with {id} not found.");
        });
    }
}
