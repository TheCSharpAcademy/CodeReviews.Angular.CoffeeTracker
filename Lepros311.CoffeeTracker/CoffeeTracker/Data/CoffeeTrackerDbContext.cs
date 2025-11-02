using CoffeeTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Data;

public class CoffeeTrackerDbContext : DbContext
{
    public CoffeeTrackerDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Coffee> Coffees { get; set; }

    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coffee>(entity =>
        {
            entity.ToTable("Coffees", t => t.HasCheckConstraint("CK_Coffees_Price", "Price > 0"));
            entity.Property(p => p.Name).IsRequired();
            entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(p => p.IsDeleted).IsRequired();
            entity.HasQueryFilter(p => !p.IsDeleted);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sales");
            entity.Property(s => s.DateAndTimeOfSale).IsRequired();
            entity.Property(s => s.Total).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(s => s.CoffeeName).IsRequired();
            entity.HasOne(s => s.Coffee).WithMany(c => c.Sales).HasForeignKey(s => s.CoffeeId).IsRequired();
            entity.HasQueryFilter(s => !s.IsDeleted);
        });

        modelBuilder.Entity<Coffee>().HasData(new List<Coffee>
        {
            new Coffee
            {
                Id = 1,
                Name= "Black Coffee",
                Price= 1.62m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 2,
                Name= "Espresso",
                Price= 4.42m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 3,
                Name= "Mocha",
                Price= 5.39m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 4,
                Name= "Latte",
                Price= 3.99m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 5,
                Name= "Cappuccino",
                Price= 7.15m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 6,
                Name= "Mazagran",
                Price= 6.47m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 7,
                Name= "Breve",
                Price= 6.33m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 8,
                Name= "Macchiato",
                Price= 5.87m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 9,
                Name= "Cortado",
                Price= 8.29m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 10,
                Name= "Dirty Chai",
                Price= 4.29m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 11,
                Name= "Irish Coffee",
                Price= 5.80m,
                IsDeleted = false,
            },

            new Coffee
            {
                Id = 12,
                Name= "Turkish Coffee",
                Price= 9.15m,
                IsDeleted = false,
            },
        });

        modelBuilder.Entity<Sale>().HasData(new List<Sale>
        {
           new Sale
            {
                Id = 1,
                DateAndTimeOfSale = new DateTime(2023, 10, 9, 10, 30, 0),
                Total = 1.62m,
                CoffeeName = "Black Coffee",
                CoffeeId = 1,
                IsDeleted = false
            }
        });
    }
}


