using EnergyDrinkTracker.Forser_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyDrinkTracker.Forser_API.DataLayer
{
    public class DrinkDbContext : DbContext
    {
        public DbSet<Records> Records { get; set; }

        public DrinkDbContext(DbContextOptions<DrinkDbContext> options) : base(options) { }
    }
}