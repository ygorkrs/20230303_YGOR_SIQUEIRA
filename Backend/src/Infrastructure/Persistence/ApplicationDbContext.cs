using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using RateMyAnimal.Application.Common.Interfaces;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<AnimalOrigin> AnimalOrigins => Set<AnimalOrigin>();
    public DbSet<AnimalTag> AnimalTags => Set<AnimalTag>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<AnimalTag>()
            .HasKey(at => new { at.AnimalId, at.TagId });

        builder.Entity<AnimalTag>()
            .HasOne(at => at.Animal)
            .WithMany(a => a.AnimalTags)
            .HasForeignKey(at => at.AnimalId);

        builder.Entity<AnimalTag>()
            .HasOne(at => at.Tag)
            .WithMany(t => t.AnimalTags)
            .HasForeignKey(at => at.TagId);

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}