namespace CafeTracker.API.Models.Dtos;

public class UpdateCafeRecord
{

    public string ProductName { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }

    public int Quantity { get; set; } 

    public DateTime DateConsumed { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }
}