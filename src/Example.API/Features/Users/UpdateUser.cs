using Example.API.Database;
using Example.API.Entities.Enums;
using MediatR;

namespace Example.API.Features.Users;

public static class UpdateUser
{
    public class Command : IRequest<bool>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
    }

    internal sealed class Handler(ExampleApplicationDbContext context) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == request.Id) ?? throw new Exception("User not found!");

            user.UpdateUser(request.FirstName, request.LastName, request.Age, request.Email, request.Gender);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("api/users", async (Command command, ISender sender) =>
        {
            var updatedUser = await sender.Send(command);

            return updatedUser ? Results.Ok(true) : Results.NotFound();
        });
    }
}