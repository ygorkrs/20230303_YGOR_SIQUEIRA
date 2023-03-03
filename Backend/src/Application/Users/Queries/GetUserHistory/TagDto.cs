using RateMyAnimal.Application.Common.Mappings;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Users.Queries.GetUserHistory;

public class TagDto : IMapFrom<Tag>
{
    public int Id { get; init; }
    public string TagName { get; init; }
}
