using NoSmoking.BBualdo.API.Models;

namespace NoSmoking.BBualdo.API.Repositories;

public interface ISmokeLogsRepository
{
  Task<IEnumerable<SmokeLog>> GetAllAsync();
  Task<SmokeLog?> GetAsync(int id);
  Task AddAsync(SmokeLog smokeLog);
  Task UpdateAsync(SmokeLog smokeLog);
  Task DeleteAsync(int id);
}
