using CoffeeTracker.Api.Data;
using CoffeeTracker.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Installers;

/// <summary>
/// Registers any database dependencies and seeds data.
/// </summary>
public static class DatabaseInstaller
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("CoffeeTracker") ?? throw new InvalidOperationException("Connection string 'CoffeeTracker' not found");

        services.AddDbContext<CoffeeTrackerDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ICoffeeTrackerRepository, CoffeeTrackerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISeederService, SeederService>();

        return services;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<CoffeeTrackerDataContext>();
        context.Database.Migrate();

        var seeder = serviceProvider.GetRequiredService<ISeederService>();
        seeder.SeedDatabase();

        return serviceProvider;
    }
}
