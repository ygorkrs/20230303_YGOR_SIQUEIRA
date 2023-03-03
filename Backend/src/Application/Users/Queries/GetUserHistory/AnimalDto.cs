using AutoMapper;
using RateMyAnimal.Application.Common.Mappings;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Users.Queries.GetUserHistory;

public class AnimalDto : IMapFrom<Animal>
{
    public AnimalDto()
    {
        Tags = new List<TagDto>();
    }

    public int Id { get; set; }
    public byte[] Photo { get; init; }
    public int UserId { get; init; }
    public int AnimalOriginId { get; init; }
    public ICollection<TagDto> Tags { get; set; }
}
