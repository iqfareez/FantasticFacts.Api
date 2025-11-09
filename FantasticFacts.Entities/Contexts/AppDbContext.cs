using Microsoft.EntityFrameworkCore;

namespace FantasticFacts.Entities.Contents;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<FantasticFact> FantasticFacts { get; set; }
}