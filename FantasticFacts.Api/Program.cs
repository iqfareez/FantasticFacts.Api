using FantasticFacts.Api.Handlers;
using FantasticFacts.Entities.Contents;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Fantastic Facts API",
                Version = "v1"
            });

            var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
            var xmlDocPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlDocPath))
            {
                options.IncludeXmlComments(xmlDocPath, includeControllerXmlComments: true);
            }
        });
        builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlite(connection); });

        var app = builder.Build();

        // Run database migrations at startup
        app.ApplyMigrations<AppDbContext>();

        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}