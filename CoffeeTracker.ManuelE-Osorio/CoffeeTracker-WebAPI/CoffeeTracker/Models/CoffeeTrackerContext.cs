using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Models;

public class CoffeeTrackerContext(DbContextOptions<CoffeeTrackerContext> options) : DbContext(options)
{
    public DbSet<CoffeeCups> Coffee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoffeeCups>( p => {
            p.Property( p => p.Quantity).IsRequired();
            p.Property( p => p.Measure).IsRequired();
            p.Property( p => p.Description).IsRequired();
            p.Property( p => p.Units).IsRequired();
            p.Property( p => p.Date).IsRequired();
        });
    }
}