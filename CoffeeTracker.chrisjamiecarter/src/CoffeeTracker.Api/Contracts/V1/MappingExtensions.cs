using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Contracts.V1;

/// <summary>
/// Provides extension methods for converting between request and response models and domain entities related to coffee records. 
/// </summary>
public static class MappingExtensions
{
    public static CoffeeRecord ToModel(this CoffeeRecordRequest request)
    {
        return new CoffeeRecord
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Date = request.Date,
        };
    }

    public static CoffeeRecordResponse ToResponse(this CoffeeRecord coffeeRecord)
    {
        return new CoffeeRecordResponse
        {
            Id = coffeeRecord.Id,
            Name = coffeeRecord.Name,
            Date = coffeeRecord.Date,
        };
    }
}
