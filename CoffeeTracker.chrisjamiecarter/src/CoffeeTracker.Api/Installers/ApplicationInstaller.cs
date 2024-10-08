using CoffeeTracker.Api.Services;

namespace CoffeeTracker.Api.Installers;

/// <summary>
/// Registers dependencies for the Application layer.
/// </summary>
public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICoffeeRecordService, CoffeeRecordService>();

        return services;
    }
}
