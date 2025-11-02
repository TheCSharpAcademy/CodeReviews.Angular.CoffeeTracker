namespace CoffeeTracker.Api.Models;

public class SaleDto
{
    public int Id { get; set; }

    public string DateAndTimeOfSale { get; set; }

    public string Total { get; set; }

    public string CoffeeName { get; set; }

    public int CoffeeId { get; set; }
}
