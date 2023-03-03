using RateMyAnimal.Application.Tags.Queries.GetTagsAvailable;
using System;

namespace RateMyAnimal.Application.Animals.Queries.GetAnimal;

public class AnimalGetDto
{
    public int Id { get; set; }
    public int OriginId { get; set; }
    public byte[] Photo { get; set; }
    public List<int> SelectedTags { get; set; } = new List<int>();
}
