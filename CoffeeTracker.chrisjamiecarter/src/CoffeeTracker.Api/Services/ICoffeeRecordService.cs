using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Services;

/// <summary>
/// Defines the contract for a service that manages <see cref="CoffeeRecord"/> entities.
/// </summary>
public interface ICoffeeRecordService
{
    Task<bool> CreateAsync(CoffeeRecord coffeeRecord);
    Task<bool> DeleteAsync(CoffeeRecord coffeeRecord);
    Task<IEnumerable<CoffeeRecord>> ReturnAsync();
    Task<CoffeeRecord?> ReturnAsync(Guid id);
    Task<bool> UpdateAsync(CoffeeRecord coffeeRecord);
}