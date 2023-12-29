using CoffeeTracker.UgniusFalze.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTrackerAPI.Repositories;

public interface ICoffeeRepository
{
    public Task AddCoffeeRecord(CoffeeRecord record);
    public Task UpdateCoffeeRecord(CoffeeRecord record);
    public Task<ActionResult<IEnumerable<CoffeeRecord>>> GetRecords();
    public Task DeleteCoffeeRecord(CoffeeRecord record);
    public bool CoffeeRecordExists(int id);
    public Task<CoffeeRecord?> GetCoffeeRecord(int id);
}