using CafeTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeTracker.API.Data;

public class AppDbContext : DbContext
{
    

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<CafeRecord> CafeRecords { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CafeRecord>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.ProductName).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Quantity).IsRequired().HasDefaultValue(1);
            entity.Property(c=> c.Notes).HasMaxLength(500);
            entity.Property(c => c.DateConsumed).IsRequired();
            entity.Property(c => c.DateCreated).IsRequired();
            entity.Property(c => c.DateModified).IsRequired();
            entity.HasIndex(c => c.ProductName);
            entity.Property(c => c.Category).IsRequired();
        });

        modelBuilder.Entity<CafeRecord>().HasData(SeedCafeRecords);
        base.OnModelCreating(modelBuilder);
    }

    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<CafeRecord>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTimeOffset.UtcNow;
                entry.Entity.DateModified = DateTimeOffset.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.DateModified = DateTimeOffset.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    private static readonly CafeRecord[] SeedCafeRecords =
    [
        new CafeRecord
        {
            Id = 1,
            ProductName = "Cappuccino",
            Category = ProductCategory.Coffee,
            Quantity = 5,
            DateConsumed = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Had with breakfast",
            DateCreated = new DateTimeOffset(2026, 5, 2, 15, 45, 20, 391, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 2, 15, 45, 20, 391, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 2,
            ProductName = "Cappuccino",
            Category = ProductCategory.Coffee,
            Quantity = 2,
            DateConsumed = new DateTime(2026, 5, 6, 11, 30, 19, 672, DateTimeKind.Utc),
            Notes = "Served with breakfast",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 29, 3, 739, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 29, 3, 739, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 3,
            ProductName = "Latte",
            Category = ProductCategory.Coffee,
            Quantity = 3,
            DateConsumed = new DateTime(2026, 5, 2, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Had with dinner",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 29, 18, 170, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 29, 18, 170, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 4,
            ProductName = "Americano",
            Category = ProductCategory.Coffee,
            Quantity = 1,
            DateConsumed = new DateTime(2026, 5, 3, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Early morning meeting prep",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 29, 30, 65, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 29, 30, 65, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 5,
            ProductName = "Mocha",
            Category = ProductCategory.Coffee,
            Quantity = 2,
            DateConsumed = new DateTime(2026, 5, 4, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Coffee break with friends",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 29, 40, 838, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 29, 40, 838, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 6,
            ProductName = "Flat White",
            Category = ProductCategory.Coffee,
            Quantity = 2,
            DateConsumed = new DateTime(2026, 5, 5, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Needed energy for coding session",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 29, 55, 56, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 29, 55, 56, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 7,
            ProductName = "Macchiato",
            Category = ProductCategory.Coffee,
            Quantity = 1,
            DateConsumed = new DateTime(2026, 5, 5, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Short coffee break",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 30, 4, 288, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 30, 4, 288, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 8,
            ProductName = "Cold Brew",
            Category = ProductCategory.ColdDrink,
            Quantity = 3,
            DateConsumed = new DateTime(2026, 5, 6, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Very hot afternoon",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 30, 13, 238, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 30, 13, 238, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 9,
            ProductName = "Irish Coffee",
            Category = ProductCategory.Coffee,
            Quantity = 1,
            DateConsumed = new DateTime(2026, 5, 6, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Relaxing evening drink",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 30, 21, 227, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 30, 21, 227, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 10,
            ProductName = "Vanilla Latte",
            Category = ProductCategory.Coffee,
            Quantity = 2,
            DateConsumed = new DateTime(2026, 5, 7, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Breakfast companion",
            DateCreated = new DateTimeOffset(2026, 5, 4, 11, 30, 28, 810, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 4, 11, 30, 28, 810, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 11,
            ProductName = "Espresso",
            Category = ProductCategory.Coffee,
            Quantity = 10,
            DateConsumed = new DateTime(2010, 10, 10, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Delicious and healthy",
            DateCreated = new DateTimeOffset(2026, 5, 6, 11, 45, 8, 57, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 6, 11, 45, 8, 57, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 13,
            ProductName = "Hollandia Yoghurt",
            Category = ProductCategory.Other,
            Quantity = 2,
            DateConsumed = new DateTime(2010, 10, 10, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Cold, creamy and tasty",
            DateCreated = new DateTimeOffset(2026, 5, 9, 14, 12, 43, 178, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 9, 14, 12, 43, 178, TimeSpan.FromHours(1))
        },
        new CafeRecord
        {
            Id = 15,
            ProductName = "Hot Choco",
            Category = ProductCategory.HotChocolate,
            Quantity = 10,
            DateConsumed = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Notes = "Cold beverage for everyday nourishment",
            DateCreated = new DateTimeOffset(2026, 5, 9, 14, 24, 10, 550, TimeSpan.FromHours(1)),
            DateModified = new DateTimeOffset(2026, 5, 9, 14, 24, 10, 550, TimeSpan.FromHours(1))
        }
    ];
}
