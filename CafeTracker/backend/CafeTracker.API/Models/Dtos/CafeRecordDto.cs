namespace CafeTracker.API.Models.Dtos;

public record CafeRecordDto(
    int Id , 
    string ProductName,
    string Category,
    int Quantity,
    DateTime DateConsumed,
    string? Notes,
    DateTimeOffset  DateCreated );