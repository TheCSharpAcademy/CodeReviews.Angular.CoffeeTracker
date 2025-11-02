namespace CoffeeTracker.Api.Models;

public class CreateSaleDto
{
    public int CoffeeId { get; set; }

    public string? DateAndTimeOfSale { get; set; }
}
