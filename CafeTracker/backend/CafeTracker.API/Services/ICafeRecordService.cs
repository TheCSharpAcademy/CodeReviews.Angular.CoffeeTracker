using CafeTracker.API.Models;
using CafeTracker.API.Models.Dtos;

namespace CafeTracker.API.Services;

public interface ICafeRecordService
{
    Task<IEnumerable<CafeRecordDto>> GetCafeRecords(string? cafeName = null, DateOnly? date = null, ProductCategory? category = null);
    Task<CafeRecordDto?> GetCafeRecordById(int id);
    Task<CafeRecordDto?> CreateCafeRecord(CreateCafeRecord createCafeRecord);
    Task DeleteCafeRecord(int id);
    Task<CafeRecordDto> UpdateCafeRecord(int id, UpdateCafeRecord updateCafeRecord);

}