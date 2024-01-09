using Microsoft.EntityFrameworkCore;
using kmakai.CoffeeTrackerAPI.Angular.Models;

namespace kmakai.CoffeeTrackerAPI.Angular;

public class TrackerContext: DbContext
{
    public TrackerContext(DbContextOptions<TrackerContext> options) : base(options)
    {
    }

    public DbSet<Coffee> Coffees { get; set; } = null!;
}
