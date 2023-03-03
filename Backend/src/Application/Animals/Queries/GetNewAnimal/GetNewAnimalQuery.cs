using MediatR;
using RateMyAnimal.Application.AnimalOrigins.Queries.GetAnimalOriginUrl;
using RateMyAnimal.Application.Tags.Queries.GetTagsAvailable;

namespace RateMyAnimal.Application.Animals.Queries.GetNewAnimal;

public record GetNewAnimalQuery : IRequest<NewAnimalDto>
{
}

public class GetNewAnimalQueryHandler : IRequestHandler<GetNewAnimalQuery, NewAnimalDto>
{
    private readonly IMediator _mediator;

    public GetNewAnimalQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<NewAnimalDto> Handle(GetNewAnimalQuery request, CancellationToken cancellationToken)
    {
        var animalOriginUrlDto = await _mediator.Send(new GetAnimalOriginUrlQuery());
        var listTagsDto = await _mediator.Send(new GetTagsAvailableQuery());

        return new NewAnimalDto
        {
            AnimalOriginUrlDto = animalOriginUrlDto,
            ListTagsDto = listTagsDto
        };
    }
}