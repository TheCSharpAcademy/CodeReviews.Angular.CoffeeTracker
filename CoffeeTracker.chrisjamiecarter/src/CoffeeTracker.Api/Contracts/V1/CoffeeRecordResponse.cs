namespace CoffeeTracker.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information for returning a CoffeeRecord as API response back to 
/// the client.
/// </summary>
public class CoffeeRecordResponse
{
    #region Properties

    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required DateTime Date { get; set; }

    #endregion
}
