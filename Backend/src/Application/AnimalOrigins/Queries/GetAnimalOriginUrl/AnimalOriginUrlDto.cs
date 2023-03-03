using RateMyAnimal.Application.Common.Mappings;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.AnimalOrigins.Queries.GetAnimalOriginUrl;

public class AnimalOriginUrlDto : IMapFrom<AnimalOrigin>
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool NeedRetrieveUrlInBody { get; init; }
}
