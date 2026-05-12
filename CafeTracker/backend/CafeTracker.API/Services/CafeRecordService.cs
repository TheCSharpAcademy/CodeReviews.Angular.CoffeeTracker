using CafeTracker.API.Data;
using CafeTracker.API.Extensions;
using CafeTracker.API.Models;
using CafeTracker.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace CafeTracker.API.Services;

public class CafeRecordService : ICafeRecordService
{
    private readonly ILogger<CafeRecordService> _logger;
    private readonly AppDbContext _dbContext;
    public CafeRecordService(ILogger<CafeRecordService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CafeRecordDto>> GetCafeRecords(string? cafeName = null, DateOnly? date = null, ProductCategory? category = null)
    {
       IQueryable<CafeRecord> query = _dbContext.CafeRecords;
       if (!string.IsNullOrEmpty(cafeName))
       {
           query = query.Where(x => x.ProductName.Contains(cafeName));
       }
       if (date.HasValue)
       {
               var startOfDay = date.Value.ToDateTime(TimeOnly.MinValue);
                var endOfDay = date.Value.ToDateTime(TimeOnly.MaxValue);
                query = query.Where(x => x.DateConsumed >= startOfDay && x.DateConsumed <= endOfDay);
       }
       if(category.HasValue)
       {
           query = query.Where(x => x.Category == category.Value);
       }
       query = query.OrderByDescending(x => x.DateConsumed);
       var records = await query.ToListAsync();
       
       var results = records.Select(record => new CafeRecordDto
       (
           Id: record.Id,
           ProductName: record.ProductName,
           Category: record.Category.GetDescription(),
           Quantity: record.Quantity,
           DateConsumed:record.DateConsumed,
           DateCreated: record.DateCreated,
           Notes: record.Notes
       )).ToList(); 
       return results;
    }

    public async Task<CafeRecordDto?> GetCafeRecordById(int id)
    {
        var record  = await _dbContext.CafeRecords.FirstOrDefaultAsync(x=> x.Id == id);
        var result = new CafeRecordDto
        (
            Id: record.Id,
            ProductName: record.ProductName,
            Category: record.Category.GetDescription(),
            Quantity: record.Quantity,
            DateConsumed:record.DateConsumed,
            DateCreated: record.DateCreated,
            Notes: record.Notes
        );
        return result;
    }

    public async Task<CafeRecordDto?> CreateCafeRecord(CreateCafeRecord createCafeRecord)
    {
        var createRecord = new CafeRecord
        {
            ProductName = createCafeRecord.ProductName,
            Category = createCafeRecord.Category,
            Quantity = createCafeRecord.Quantity,
            DateConsumed = createCafeRecord.DateConsumed,
            Notes = createCafeRecord.Notes,
        };
        await _dbContext.CafeRecords.AddAsync(createRecord);
        await _dbContext.SaveChangesAsync();
        
        var recordToReturn = new CafeRecordDto
        (
            Id: createRecord.Id,
            ProductName: createRecord.ProductName,
            Category: createRecord.Category.GetDescription(),
            Quantity: createRecord.Quantity,
            DateConsumed:createRecord.DateConsumed,
            DateCreated: createRecord.DateCreated,
            Notes: createRecord.Notes
        );

        return recordToReturn;
    }

    public async Task DeleteCafeRecord(int id)
    {
        var recordExists  = await _dbContext.CafeRecords.FirstOrDefaultAsync(x => x.Id == id);
        if (recordExists is null)
            throw new InvalidOperationException("Cafe record could not be deleted");
        _dbContext.CafeRecords.Remove(recordExists);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<CafeRecordDto> UpdateCafeRecord(int id, UpdateCafeRecord updateCafeRecord)
    {
        var recordExists = await _dbContext.CafeRecords.FirstOrDefaultAsync(x => x.Id == id);
        if (recordExists is null)
            throw new ArgumentException("Cafe record does not exist");
        
        recordExists.ProductName = updateCafeRecord.ProductName;
        recordExists.Category = updateCafeRecord.Category;
        recordExists.Quantity = updateCafeRecord.Quantity; 
        recordExists.DateConsumed = updateCafeRecord.DateConsumed;
        recordExists.Notes = updateCafeRecord.Notes;
        recordExists.DateModified = DateTimeOffset.UtcNow;
        
        await _dbContext.SaveChangesAsync();

        var result = new CafeRecordDto
        (
            Id: recordExists.Id,
            ProductName: recordExists.ProductName,
            Category: recordExists.Category.GetDescription(),
            Quantity: recordExists.Quantity,
            DateConsumed:recordExists.DateConsumed,
            DateCreated: recordExists.DateCreated,
            Notes: recordExists.Notes
        );
        return result;

    }
}