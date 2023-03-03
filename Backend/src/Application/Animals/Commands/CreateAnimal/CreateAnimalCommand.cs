using MediatR;
using RateMyAnimal.Application.Common.Interfaces;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Animals.Commands.CreateAnimal;

public record CreateAnimalCommand : IRequest<int>
{
    public int User { get; init; }
    public int Origin { get; init; }
    public List<int> Tags { get; init; }
    public byte[] Photo { get; init; }
}

public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAnimalCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        var entity = new Animal
        {
            AnimalOriginId = request.Origin,
            UserId = request.User,
            Photo = request.Photo,
            Created = DateTime.UtcNow,
            CreatedBy = request.User.ToString(),
        };

        _context.Animals.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        List<AnimalTag> animalTags = new List<AnimalTag>();
        foreach (var tagId in request.Tags)
        {
            animalTags.Add(new AnimalTag
            {
                AnimalId = entity.Id,
                TagId = tagId
            });
        }

        _context.AnimalTags.AddRange(animalTags);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}