using System.Text.Json.Serialization;

namespace CoffeeTracker.Api.Models;

public class CoffeeDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Price { get; set; }
}
