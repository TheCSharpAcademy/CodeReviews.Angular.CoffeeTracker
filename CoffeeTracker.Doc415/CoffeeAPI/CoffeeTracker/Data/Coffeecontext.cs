using CoffeeTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Data;


public class Coffeecontext : DbContext
{
    public Coffeecontext(DbContextOptions<Coffeecontext> options) : base(options) { }
    public DbSet<Coffee> CoffeeSet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define coffee types with avatars
        var coffeeTypes = new List<(string Name, string Avatar)>
        {
            ("Turkish Coffee", "turkish_k.jpg"),
            ("Cappuccino", "cappuccino_k.jpg"),
            ("Latte", "latte_k.jpg"),
            ("Mocha", "mocha_k.jpg"),
            ("Espresso", "expresso_k.jpg"),
            ("Americano", "americano_k.jpg"),
            ("Filtered Coffee", "filtered_k.jpg")
        };

        var random = new Random();

        var coffeeEntries = new List<Coffee>();
        for (int i = 0; i < 20; i++)
        {
            var coffeeType = coffeeTypes[random.Next(coffeeTypes.Count)];
            var consumptionDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(0, 7))).ToString("yyyy-MM-dd");

            coffeeEntries.Add(new Coffee
            {
                Id = Guid.NewGuid().ToString(), 
                Name = coffeeType.Name,
                Avatar = coffeeType.Avatar,
                ConsumptionDate = consumptionDate
            });
        }
  
        modelBuilder.Entity<Coffee>().HasData(coffeeEntries);
    }
}
