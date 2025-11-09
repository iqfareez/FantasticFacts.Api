using FantasticFacts.Api.Handlers;
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
        builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlite(connection); });

        var app = builder.Build();

        // Run database migrations at startup
        app.ApplyMigrations<AppDbContext>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}