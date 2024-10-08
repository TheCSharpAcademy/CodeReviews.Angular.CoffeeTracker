using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Data;

/// <summary>
/// The UnitOfWork class provides a central point for managing database transactions and saving
/// changes across repositories.
/// </summary>
/// <remarks>
/// This class follows the Unit of Work design pattern, ensuring that all repository operations 
/// are treated as a single transaction, maintaining data consistency.
/// </remarks>
public class UnitOfWork : IUnitOfWork
{
    #region Fields

    private readonly CoffeeTrackerDataContext _dataContext;

    #endregion
    #region Constructors

    public UnitOfWork(CoffeeTrackerDataContext dataContext, ICoffeeTrackerRepository repository)
    {
        _dataContext = dataContext;
        CoffeeRecord = repository;
    }

    #endregion
    #region Properties

    public ICoffeeTrackerRepository CoffeeRecord { get; set; }

    #endregion
    #region Methods

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _dataContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    #endregion
}
