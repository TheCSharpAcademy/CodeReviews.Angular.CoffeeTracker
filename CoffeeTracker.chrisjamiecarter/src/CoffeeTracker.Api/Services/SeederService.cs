using Bogus;
using CoffeeTracker.Api.Data;
using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Services;

/// <summary>
/// Provides methods to seed the database with initial data.
/// This service adds a defined set of default Categories and a set of fake Friends using Bogus.
/// </summary>
public class SeederService : ISeederService
{
    #region Fields

    private readonly string[] _coffees =
    [
        "Affogato",
        "Affogato al Caffe",
        "Black Americano",
        "Breve",
        "Cafe au Lait",
        "Cafe Cubano",
        "Caffe Crema",
        "Caffe Marocchino",
        "Cappuccino",
        "Chicory Coffee",
        "Cold Brew",
        "Cortado",
        "Double Espresso",
        "Espresso",
        "Espresso con Panna",
        "Espresso Macchiato",
        "Flat White",
        "Frappe",
        "Iced Americano",
        "Iced Coffee",
        "Iced Latte",
        "Iced Macchiato",
        "Irish Coffee",
        "Latte",
        "Latte Macchiato",
        "Lungo",
        "Mocha",
        "Nitro Cold Brew",
        "Piccolo Latte",
        "Pumpkin Spice Latte",
        "Ristretto",
        "Spiced Coffee",
        "Turkish Coffee",
        "Vienna Coffee",
        "White Americano",
    ];

    private readonly CoffeeTrackerDataContext _context;

    #endregion
    #region Constructors

    public SeederService(CoffeeTrackerDataContext context)
    {
        _context = context;
    }

    #endregion
    #region Methods

    public void SeedDatabase()
    {
        SeedCoffeeRecords();
    }

    private void SeedCoffeeRecords()
    {
        if (_context.CoffeeRecord.Any())
        {
            return;
        }

        Randomizer.Seed = new Random(19890309);

        var fakeData = new Faker<CoffeeRecord>()
            .RuleFor(d => d.Id, f => f.Random.Guid())
            .RuleFor(d => d.Name, f => f.PickRandom(_coffees))
            .RuleFor(d => d.Date, f => f.Date.Past(1));

        foreach (var fake in fakeData.Generate(100))
        {
            _context.CoffeeRecord.Add(fake);
        }

        _context.SaveChanges();
    }

    #endregion
}
