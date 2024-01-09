using EnergyDrinkTracker.Forser_API.DataLayer;
using EnergyDrinkTracker.Forser_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyDrinkTracker.Forser_API.Repositories
{
    public class RecordRepository : GenericRepository<Records>, IRecordRepository
    {
        private readonly DrinkDbContext _context;

        public RecordRepository(DrinkDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Records> CreateRecordAsync(Records record)
        {
            var result = _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();
            return result.Result.Entity;
        }

        public async Task<List<Records>> GetAllRecordsAsync()
        {
            return await _context.Records.OrderByDescending(o => o.DrinkDate).ToListAsync();
        }

        public async Task<Records> GetRecordByIdAsync(int id)
        {
            return await _context.Records.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public bool DeleteRecordById(int recordId)
        {
            Records record = _context.Records.Find(recordId);

            if (record != null)
            {
                _context.Remove(record);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Records> UpdateRecordAsync(Records record)
        {
            var result = await _context.Records.FirstOrDefaultAsync(e => e.Id == record.Id);

            if (result != null)
            {
                result.DrinkDate = record.DrinkDate;
                result.EnergyDrink = record.EnergyDrink;
                result.CanSize = record.CanSize;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}