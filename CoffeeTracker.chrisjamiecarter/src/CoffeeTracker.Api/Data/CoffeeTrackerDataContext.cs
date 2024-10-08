using CoffeeTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Data;

/// <summary>
/// Represents the Entity Framework Core database context for the CoffeeTracker data store.
/// </summary>
public class CoffeeTrackerDataContext : DbContext
{
    #region Constructors

    public CoffeeTrackerDataContext(DbContextOptions<CoffeeTrackerDataContext> options) : base(options) { }

    #endregion
    #region Properties

    public DbSet<CoffeeRecord> CoffeeRecord { get; set; } = default!;

    #endregion
}
