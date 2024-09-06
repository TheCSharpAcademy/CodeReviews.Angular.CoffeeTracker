using System.ComponentModel.DataAnnotations;

namespace CoffeeTracker.Models;

public class Coffee
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public string ConsumptionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now).ToShortDateString();
}
