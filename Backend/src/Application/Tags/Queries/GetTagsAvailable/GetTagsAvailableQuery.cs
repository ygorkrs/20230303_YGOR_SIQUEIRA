using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RateMyAnimal.Application.Common.Interfaces;

namespace RateMyAnimal.Application.Tags.Queries.GetTagsAvailable;

public record GetTagsAvailableQuery : IRequest<ListTagsDto>
{
}

public class GetTagsAvailableQueryHandler : IRequestHandler<GetTagsAvailableQuery, ListTagsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTagsAvailableQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListTagsDto> Handle(GetTagsAvailableQuery request, CancellationToken cancellationToken)
    {
        return new ListTagsDto
        {
            ListTagsAvailable = await _context.Tags
            .ProjectTo<TagsAvailableDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.TagName)
            .ToListAsync(cancellationToken)
        };
    }
}