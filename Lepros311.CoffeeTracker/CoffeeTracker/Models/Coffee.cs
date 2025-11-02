namespace CoffeeTracker.Api.Models;

public class Coffee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public List<Sale> Sales { get; set; }

    public bool IsDeleted { get; set; } = false;
}
