using Coffee_Tracker_Angular.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Coffee_Tracker_Angular.Database
{
    public class CoffeeContext : DbContext
    {
        public CoffeeContext( DbContextOptions<CoffeeContext> options ) : base(options) { }


        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CoffeeRecords>().HasData(
                new CoffeeRecords { Id = 1, Quantity = 100, Date = DateOnly.FromDateTime(DateTime.Today) },
                new CoffeeRecords { Id = 2, Quantity = 101, Date = DateOnly.FromDateTime(DateTime.Today) },
                new CoffeeRecords { Id = 3, Quantity = 100, Date = DateOnly.FromDateTime(DateTime.Today.AddDays(40)) },
                new CoffeeRecords { Id = 4, Quantity = 101, Date = DateOnly.FromDateTime(DateTime.Today.AddMonths(3)) }
            );

        }


        public DbSet<CoffeeRecords> CoffeeRecords { get; set; }
    }
}
