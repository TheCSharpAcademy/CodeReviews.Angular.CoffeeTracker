using Microsoft.EntityFrameworkCore;
using NoSmoking.BBualdo.API.Models;

namespace NoSmoking.BBualdo.API.Data;

public class SmokeLogsContext(DbContextOptions<SmokeLogsContext> options) : DbContext(options)
{
  public DbSet<SmokeLog> Records { get; set; }
}
