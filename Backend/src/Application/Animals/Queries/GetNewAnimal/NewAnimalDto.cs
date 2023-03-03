using RateMyAnimal.Application.AnimalOrigins.Queries.GetAnimalOriginUrl;
using RateMyAnimal.Application.Tags.Queries.GetTagsAvailable;

namespace RateMyAnimal.Application.Animals.Queries.GetNewAnimal;

public class NewAnimalDto
{
    public AnimalOriginUrlDto AnimalOriginUrlDto { get; set; }
    public ListTagsDto ListTagsDto { get; set; }
    public byte[] Photo { get; set; }
}
