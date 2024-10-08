namespace CoffeeTracker.Api.Data;

/// <summary>
/// Represents a unit of work pattern interface for coordinating changes across multiple repositories in the Application.
/// </summary>
public interface IUnitOfWork
{
    ICoffeeTrackerRepository CoffeeRecord { get; set; }
    Task<int> SaveAsync();
}