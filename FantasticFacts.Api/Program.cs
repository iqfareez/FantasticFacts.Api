using FantasticFacts.Entities.Contents;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FantasticFacts.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Create and open an in-memory SQLite connection (kept open for app lifetime)
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connection);
        });

        var app = builder.Build();
        
        // Run database migrations at startup
        using (var scope = app.Services.CreateScope())
        {
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (appDbContext.Database.GetPendingMigrations().Any())
            {
                appDbContext.Database.Migrate();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}