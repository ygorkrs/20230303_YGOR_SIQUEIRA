using MediatR;
using RateMyAnimal.Application.Common.Interfaces;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<int>
{
    public string UserIdentification { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User
        {
            UserIdentification = request.UserIdentification,
            Created = DateTime.UtcNow,
            CreatedBy = "API"
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}