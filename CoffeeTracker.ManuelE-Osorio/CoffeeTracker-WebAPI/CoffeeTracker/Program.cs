using CoffeeTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker;

public class CoffeeTracker
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();
        builder.Services.AddDbContext<CoffeeTrackerContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeTrackerConnectionString") ?? 
                throw new InvalidOperationException("Connection string 'CoffeeTracker' not found.")));
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowAnyOrigin",
                policy  =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
        });


        var app = builder.Build();

        using (var context = new CoffeeTrackerContext( 
            app.Services.CreateScope().ServiceProvider.GetRequiredService<DbContextOptions<CoffeeTrackerContext>>()))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SeedData.SeedCoffeeCups(context);
            }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAnyOrigin");
        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}




