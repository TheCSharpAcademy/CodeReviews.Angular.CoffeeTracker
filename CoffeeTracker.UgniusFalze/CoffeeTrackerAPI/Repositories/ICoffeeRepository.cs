using CoffeeTracker.UgniusFalze.Models;

namespace CoffeeTrackerAPI.Repositories;

public interface ICoffeeRepository
{
    public void AddCoffeeRecord(CoffeeRecord record);
    public void UpdateCoffeeRecord(CoffeeRecord record);
    public IEnumerable<CoffeeRecord> GetRecords();
    public void DeleteCoffeeRecord(int id);
    bool CoffeeRecordExists(int id);
}