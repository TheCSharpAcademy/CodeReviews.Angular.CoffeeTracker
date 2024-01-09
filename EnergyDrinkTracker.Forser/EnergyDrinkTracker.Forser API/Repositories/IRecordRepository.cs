using EnergyDrinkTracker.Forser_API.Models;

namespace EnergyDrinkTracker.Forser_API.Repositories
{
    public interface IRecordRepository : IGenericRepository<Records> 
    {
        Task<Records> CreateRecordAsync(Records record);
        Task<List<Records>> GetAllRecordsAsync();
        bool DeleteRecordById(int id);
        Task<Records> UpdateRecordAsync(Records record);
        Task<Records> GetRecordByIdAsync(int id);
    }
}