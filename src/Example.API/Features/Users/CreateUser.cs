using Example.API.Database;
using Example.API.Entities;
using Example.API.Entities.Enums;
using FluentValidation;
using MediatR;

namespace Example.API.Features.Users;

public static class CreateUser
{
    public class Command : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.Age).GreaterThan(0);
        }
    }

    internal sealed class Handler(ExampleApplicationDbContext context, IValidator<Command> validator) : IRequestHandler<Command, int>
    {
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new Exception(validationResult.ToString());

            var user = new User(request.FirstName, request.LastName, request.Age, request.Email, request.Gender);

            context.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }

    public static void MapEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/user", async (Command command, ISender sender) =>
        {
            var userId = await sender.Send(command);

            return Results.Ok(userId);
        });
    }
}