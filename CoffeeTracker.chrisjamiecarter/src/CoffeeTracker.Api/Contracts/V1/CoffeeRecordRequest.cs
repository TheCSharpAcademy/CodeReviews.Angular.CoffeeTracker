namespace CoffeeTracker.Api.Contracts.V1;

/// <summary>
/// Represents only the necessary information required from API requests to create or update a CoffeeRecord.
/// </summary>
public class CoffeeRecordRequest
{
    #region Properties

    public required string Name { get; set; }

    public required DateTime Date { get; set; }

    #endregion
}
