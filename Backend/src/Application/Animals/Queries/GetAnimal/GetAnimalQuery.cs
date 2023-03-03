using MediatR;
using Microsoft.EntityFrameworkCore;
using RateMyAnimal.Application.Common.Exceptions;
using RateMyAnimal.Application.Common.Interfaces;
using RateMyAnimal.Application.Users.Queries.GetUserHistory;
using RateMyAnimal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyAnimal.Application.Animals.Queries.GetAnimal;

public record GetAnimalQuery : IRequest<AnimalGetDto>
{
    public int Id { get; set; }
}

public class GetAnimalQueryHandler : IRequestHandler<GetAnimalQuery, AnimalGetDto>
{
    private readonly IApplicationDbContext _context;

    public GetAnimalQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AnimalGetDto> Handle(GetAnimalQuery request, CancellationToken cancellationToken)
    {
        var animalTag = await _context.Animals
                .Include(at => at.AnimalTags)
                    .ThenInclude(t => t.Tag)
                .FirstAsync(x => x.Id == request.Id);

        if (animalTag == null)
        {
            throw new NotFoundException(nameof(Animal), request.Id);
        }

        var animalDto = new AnimalGetDto
        {
            Id = request.Id,
            OriginId = animalTag.AnimalOriginId,
            Photo = animalTag.Photo,
        };

        foreach (var tag in animalTag.AnimalTags)
        {
            animalDto.SelectedTags.Add(tag.Tag.Id);
        }

        return animalDto;
    }
}