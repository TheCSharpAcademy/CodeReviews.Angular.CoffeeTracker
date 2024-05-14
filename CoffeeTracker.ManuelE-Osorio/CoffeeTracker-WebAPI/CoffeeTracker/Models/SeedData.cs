namespace CoffeeTracker.Models;

public class SeedData
{
    public static void SeedCoffeeCups( CoffeeTrackerContext context)
    {
        if(context.Coffee.Any())
            return;
        context.Coffee.AddRange([
            new CoffeeCups {
                Quantity = 4,
                Measure = 500,
                Description = "4 cups of coffee",
                Units = CoffeeMeasureUnits.ml,
                Date =  new DateTime(2024, 04, 04, 0, 0, 0)
            },
            new CoffeeCups {
                Quantity = 2,
                Measure = 8,
                Description = "2 cups of coffee",
                Units = CoffeeMeasureUnits.oz,
                Date = new DateTime(2024, 04, 05, 0, 0, 0)
            },
            new CoffeeCups {
                Quantity = 3,
                Measure = 500,
                Description = "3 cups of coffee",
                Units = CoffeeMeasureUnits.ml,
                Date = new DateTime(2024, 04, 06, 0, 0, 0)
            },
            new CoffeeCups {
                Quantity = 4,
                Measure = 12,
                Description = "4 cups of coffee",
                Units = CoffeeMeasureUnits.oz,
                Date = new DateTime(2024, 04, 7, 0, 0, 0)
            },
            new CoffeeCups {
                Quantity = 4,
                Measure = 500,
                Description = "4 cups of coffee",
                Units = CoffeeMeasureUnits.ml,
                Date = new DateTime(2024, 04, 8, 0, 0, 0)
            },
            new CoffeeCups {
                Quantity = 1,
                Measure = 400,
                Description = "1 cups of coffee",
                Units = CoffeeMeasureUnits.ml,
                Date = new DateTime(2024, 04, 9, 0, 0, 0)
            },
        ]);
        context.SaveChanges();
    }
}