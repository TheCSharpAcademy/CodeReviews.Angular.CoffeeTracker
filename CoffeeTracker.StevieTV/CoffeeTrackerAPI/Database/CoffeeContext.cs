using CoffeeTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTrackerAPI.Database;

public class CoffeeContext : DbContext
{
    public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options)
    {
    }

    public DbSet<Coffee> Coffees { get; set; } = null!;

}