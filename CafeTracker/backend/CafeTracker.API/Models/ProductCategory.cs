using System.ComponentModel;

namespace CafeTracker.API.Models;

public enum ProductCategory
{
    [Description("None")]
    None = 0,
    [Description("Coffee")]
    Coffee = 1,
    [Description("Tea")]
    Tea,
    [Description("Hot Chocolate")]
    HotChocolate,
    [Description("Cold Drink")]
    ColdDrink,
    [Description("Pastry")]
    Pastry,
    [Description("Sandwich")]
    Sandwich,
    [Description("Cake")]
    Cake,
    [Description("Other")]
    Other
}