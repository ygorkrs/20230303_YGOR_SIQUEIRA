namespace RateMyAnimal.Domain.Entities;

public class AnimalOrigin : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Url { get; set; }
    public bool NeedRetrieveUrlInBody { get; set; }
}
