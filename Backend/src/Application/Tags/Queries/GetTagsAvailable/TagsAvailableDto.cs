using RateMyAnimal.Application.Common.Mappings;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Tags.Queries.GetTagsAvailable;

public class TagsAvailableDto : IMapFrom<Tag>
{
    public int Id { get; init; }
    public string TagName { get; init; }
}
