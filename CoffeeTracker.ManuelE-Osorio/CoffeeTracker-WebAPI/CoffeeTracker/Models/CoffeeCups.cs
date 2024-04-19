using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTracker.Models;

public class CoffeeCups
{
    public int Id {get; set;}
    
    [Required]
    [Range(0, int.MaxValue, MinimumIsExclusive = true)]
    public int? Quantity {get; set;}

    [Required]
    [Range(0, int.MaxValue, MinimumIsExclusive = true)]
    public int? Measure {get; set;}
    
    public string? Description {get; set;}

    [Required]
    [EnumDataType(typeof(CoffeeMeasureUnits))]
    public CoffeeMeasureUnits? Units { get; set;}

    [Required]
    [DataType(DataType.Date)]
    public DateTime? Date {get; set;}
}