using CoffeeTracker.Data;
using CoffeeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeesController : ControllerBase
    {
        private readonly Coffeecontext _context;

        public CoffeesController(Coffeecontext context)
        {
            _context = context;
        }

        // GET: api/Coffees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coffee>>> GetCoffeeSet()
        {
            return await _context.CoffeeSet.ToListAsync();
        }

        // GET: api/Coffees/"2024-02-01"
        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<Coffee>>> GetCoffeeByDate(string date)
        {
            var coffees = await _context.CoffeeSet.Where(coffee => coffee.ConsumptionDate == date).ToListAsync();

            if (coffees == null)
            {
                return NotFound();
            }

            return coffees;
        }

        [HttpGet("/withid/{id}")]
        public async Task<ActionResult<Coffee>> GetCoffee(string id)
        {
            var coffee = await _context.CoffeeSet.FindAsync(id);

            if (coffee == null)
            {
                return NotFound();
            }

            return coffee;
        }

        // PUT: api/Coffees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffee(string id, Coffee coffee)
        {
            if (id != coffee.Id)
            {
                return BadRequest();
            }

            _context.Entry(coffee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoffeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coffees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coffee>> PostCoffee(Coffee coffee)
        {
            coffee.Id = Guid.NewGuid().ToString();
            coffee.ConsumptionDate = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            _context.CoffeeSet.Add(coffee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoffee", new { id = coffee.Id }, coffee);
        }

        // DELETE: api/Coffees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffee(string id)
        {
            var coffee = await _context.CoffeeSet.FindAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }

            _context.CoffeeSet.Remove(coffee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoffeeExists(string id)
        {
            return _context.CoffeeSet.Any(e => e.Id == id);
        }
    }
}
