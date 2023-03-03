using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyAnimal.Domain.Entities;

public class Animal : BaseAuditableEntity
{
    [Column(TypeName = "varbinary(MAX)")]
    public byte[] Photo { get; set; }
    public int AnimalOriginId { get; set; }
    public AnimalOrigin AnimalOrigin { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<AnimalTag> AnimalTags { get; set; } = new HashSet<AnimalTag>();
}