namespace RateMyAnimal.Domain.Entities;

public class AnimalTag
{
    public int AnimalId { get; set; }
    public Animal Animal { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
