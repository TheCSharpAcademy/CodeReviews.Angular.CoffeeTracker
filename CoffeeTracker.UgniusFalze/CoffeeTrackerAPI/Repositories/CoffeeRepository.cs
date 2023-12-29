using CoffeeTracker.UgniusFalze.Models;

namespace CoffeeTrackerAPI.Repositories;

public class CoffeeRepository(CoffeeRecordContext coffeeRecordContext) : ICoffeeRepository
{
    private CoffeeRecordContext CoffeeRecordContext { get; set; } = coffeeRecordContext;

    public IEnumerable<CoffeeRecord> GetRecords()
    {
        throw new NotImplementedException();
    }

    public void AddCoffeeRecord(CoffeeRecord record)
    {
        throw new NotImplementedException();
    }

    public bool CoffeeRecordExists(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteCoffeeRecord(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateCoffeeRecord(CoffeeRecord record)
    {
        throw new NotImplementedException();
    }
}