using MediatR;
using Microsoft.EntityFrameworkCore;
using RateMyAnimal.Application.Animals.Commands.CreateAnimal;
using RateMyAnimal.Application.Common.Exceptions;
using RateMyAnimal.Application.Common.Interfaces;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Animals.Commands.UpdateAnimal;

public record UpdateAnimalCommand : IRequest<int>
{
    public int Id { get; init; }
    public int User { get; init; }
    public List<int> Tags { get; init; }
}

public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateAnimalCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Animals
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Animal), request.Id);
        }

        entity.LastModified = DateTime.UtcNow;
        entity.LastModifiedBy = request.User.ToString();

        await _context.AnimalTags
            .Where(a => a.AnimalId == request.Id)
            .ForEachAsync(at => _context.AnimalTags.Remove(at));

        List<AnimalTag> animalTags = new List<AnimalTag>();
        foreach (var tagId in request.Tags)
        {
            animalTags.Add(new AnimalTag
            {
                AnimalId = request.Id,
                TagId = tagId,
            });
        }

        _context.AnimalTags.AddRange(animalTags);

        await _context.SaveChangesAsync(cancellationToken);

        return request.Id;

    }
}