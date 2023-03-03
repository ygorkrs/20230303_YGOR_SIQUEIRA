using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RateMyAnimal.Domain.Entities;

namespace RateMyAnimal.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    public ApplicationDbContextInitialiser(ApplicationDbContext context, ILogger<ApplicationDbContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        bool needToSave = false;
        
        if (!_context.Tags.Any())
        {
            needToSave = true;
            _context.Tags.AddRange(new List<Tag>()
            {
                new Tag { TagName = "Cute", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" },
                new Tag { TagName = "Happy", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" },
                new Tag { TagName = "Ugly", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" },
                new Tag { TagName = "Sad", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" },
                new Tag { TagName = "Big", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" },
                new Tag { TagName = "Small", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" }
            });            
        }

        if (!_context.AnimalOrigins.Any())
        {
            needToSave = true;
            _context.AnimalOrigins.AddRange(new List<AnimalOrigin>()
            {
                new AnimalOrigin { 
                    Name = "CatAaS", Url = "https://cataas.com/cat", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" 
                },
                new AnimalOrigin { 
                    Name = "PlaceDog", Url = "https://place.dog/300/200", Created = DateTime.UtcNow, CreatedBy = "SYSTEM" 
                },
                new AnimalOrigin { 
                    Name = "Shibe", Url = "https://shibe.online/api/shibes", NeedRetrieveUrlInBody = true, Created = DateTime.UtcNow, CreatedBy = "SYSTEM" 
                },
            });
        }

        if (needToSave)
            await _context.SaveChangesAsync();
    }
}