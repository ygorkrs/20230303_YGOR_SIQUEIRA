using Microsoft.EntityFrameworkCore;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Animal> Animals { get; }
    DbSet<AnimalOrigin> AnimalOrigins { get; }
    DbSet<AnimalTag> AnimalTags { get; }
    DbSet<Tag> Tags { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
