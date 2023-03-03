using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RateMyAnimal.Application.Common.Interfaces;

namespace RateMyAnimal.Application.Users.Queries.GetUserHistory;

public record GetUserHistoryQuery : IRequest<UserHistoryDto>
{
    public int UsertId { get; set; }
}

public class GetUserHistoryQueryHandler : IRequestHandler<GetUserHistoryQuery, UserHistoryDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserHistoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserHistoryDto> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
    {
        var userHistoryDto = new UserHistoryDto();

        var listAnimals = await _context.Animals
                .Include(at => at.AnimalTags)
                    .ThenInclude(t => t.Tag)
                .Where(x => x.UserId == request.UsertId)
                .OrderByDescending(x => x.LastModified)
                .OrderByDescending(x => x.Created)
                .ToListAsync(cancellationToken);

        foreach (var animalTag in listAnimals)
        {
            var animalDto = new AnimalDto
            {
                Id = animalTag.Id,
                AnimalOriginId = animalTag.AnimalOriginId,
                UserId = animalTag.UserId,
                Photo = animalTag.Photo,
            };

            foreach(var tag in animalTag.AnimalTags)
            {
                animalDto.Tags.Add(new TagDto
                {
                    TagName = tag.Tag.TagName,
                    Id = tag.Tag.Id,
                });
            }

            userHistoryDto.Animals.Add(animalDto);
        }

        return userHistoryDto;
    }
}
