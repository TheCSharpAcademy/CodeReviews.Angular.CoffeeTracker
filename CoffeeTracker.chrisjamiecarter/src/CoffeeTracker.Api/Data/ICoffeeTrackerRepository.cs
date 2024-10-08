using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Data;

/// <summary>
/// Defines the contract for performing CRUD operations on <see cref="CoffeeRecord"/> entities in the
/// data store.
/// </summary>
public interface ICoffeeTrackerRepository
{
    Task CreateAsync(CoffeeRecord coffeeRecord);
    Task DeleteAsync(CoffeeRecord coffeeRecord);
    Task<IEnumerable<CoffeeRecord>> ReturnAsync();
    Task<CoffeeRecord?> ReturnAsync(Guid id);
    Task UpdateAsync(CoffeeRecord coffeeRecord);
}