using MediatR;
using RateMyAnimal.Application.Common.Interfaces;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Tags.Commands.CreateTag;

public record CreateTagCommand : IRequest<int>
{
    public string TagName { get; init; }
    public int UserId { get; init; }
}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new Tag
        {
            TagName = request.TagName,
            Created = DateTime.UtcNow,
            CreatedBy = request.UserId.ToString(),
        };

        _context.Tags.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
