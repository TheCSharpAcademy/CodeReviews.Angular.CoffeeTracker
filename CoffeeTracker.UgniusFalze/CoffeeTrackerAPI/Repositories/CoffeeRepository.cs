using CoffeeTracker.UgniusFalze.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTrackerAPI.Repositories;

public class CoffeeRepository(CoffeeRecordContext coffeeRecordContext) : ICoffeeRepository
{
    private CoffeeRecordContext CoffeeRecordContext { get;} = coffeeRecordContext;

    public async Task<ActionResult<IEnumerable<CoffeeRecord>>> GetRecords()
    {
        return await CoffeeRecordContext.CoffeeRecords.ToListAsync();
    }

    public async Task AddCoffeeRecord(CoffeeRecord record)
    {
        CoffeeRecordContext.CoffeeRecords.Add(record);
        await SaveChanges();
    }

    public bool CoffeeRecordExists(int id)
    {
        return CoffeeRecordContext.CoffeeRecords.Any(records => records.CoffeeRecordId == id);
    }

    public async Task DeleteCoffeeRecord(CoffeeRecord record)
    {
        CoffeeRecordContext.CoffeeRecords.Remove(record);
        await SaveChanges();
    }

    public async Task UpdateCoffeeRecord(CoffeeRecord record)
    {
        CoffeeRecordContext.Entry(record).State = EntityState.Modified;
        await SaveChanges();
    }

    public async Task<CoffeeRecord?> GetCoffeeRecord(int id)
    {
        return await CoffeeRecordContext.CoffeeRecords.FindAsync(id);
    }

    private async Task SaveChanges()
    {
        await CoffeeRecordContext.SaveChangesAsync();
    }
}