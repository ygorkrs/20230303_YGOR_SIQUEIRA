namespace RateMyAnimal.Domain.Entities;

public class Tag : BaseAuditableEntity
{
    public string TagName { get; set; }
    public ICollection<AnimalTag> AnimalTags { get; set; } = new HashSet<AnimalTag>();
}
