using EnergyDrinkTracker.Forser_API.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace EnergyDrinkTracker.Forser_API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DrinkDbContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(DrinkDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task CreateAsync(T entity) => await _context.AddAsync(entity);

        public async Task DeleteAsync(int id)
        {
            T exisiting = await _entities.FindAsync(id);
            _entities.Remove(exisiting);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}