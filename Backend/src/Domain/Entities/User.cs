namespace RateMyAnimal.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string UserIdentification { get; set; }
    public ICollection<Animal> Animals { get; set; } = new HashSet<Animal>();
}