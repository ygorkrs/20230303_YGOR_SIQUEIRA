using RateMyAnimal.Application.Common.Mappings;
using RateMyAnimal.Domain.Entities;


namespace RateMyAnimal.Application.Users.Queries.GetUserHistory;

public class UserHistoryDto : IMapFrom<User>
{
    public UserHistoryDto() 
    { 
        Animals = new List<AnimalDto>();
    }
    public ICollection<AnimalDto> Animals { get; set; }
}
