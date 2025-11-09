using Microsoft.EntityFrameworkCore;

namespace FantasticFacts.Api.Handlers;

public static class MigrationHandler
{
    public static void ApplyMigrations<T>(this WebApplication app) where T : DbContext
    {
        using var scope = app.Services.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<T>();
        if (appDbContext.Database.GetPendingMigrations().Any())
        {
            appDbContext.Database.Migrate();
        }
    }
    
}