namespace CafeTracker.API.Models;

public class CafeRecord
{
    public int Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; } 
    public ProductCategory Category { get; set; }

    public DateTime DateConsumed { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }
    public DateTimeOffset  DateCreated { get; set; }
    public DateTimeOffset DateModified { get; set; }
}