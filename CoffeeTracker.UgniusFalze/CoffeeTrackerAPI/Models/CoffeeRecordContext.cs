using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.UgniusFalze.Models;

public class CoffeeRecordContext : DbContext
{
    public CoffeeRecordContext(DbContextOptions<CoffeeRecordContext> options):base(options)
    {
        
    }
    
    public DbSet<CoffeeRecord> CoffeeRecords { get; set; }
}