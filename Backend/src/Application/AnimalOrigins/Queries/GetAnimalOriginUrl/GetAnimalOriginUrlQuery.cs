using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RateMyAnimal.Application.Common.Interfaces;

namespace RateMyAnimal.Application.AnimalOrigins.Queries.GetAnimalOriginUrl;

public record GetAnimalOriginUrlQuery : IRequest<AnimalOriginUrlDto>
{
}

public class GetAnimalOriginUrlQueryHandler : IRequestHandler<GetAnimalOriginUrlQuery, AnimalOriginUrlDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAnimalOriginUrlQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AnimalOriginUrlDto> Handle(GetAnimalOriginUrlQuery request, CancellationToken cancellationToken)
    {
        Random rand = new Random();
        int skipper = rand.Next(0, _context.AnimalOrigins.Count());

        return await _context.AnimalOrigins
            .Skip(skipper)
            .ProjectTo<AnimalOriginUrlDto>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}