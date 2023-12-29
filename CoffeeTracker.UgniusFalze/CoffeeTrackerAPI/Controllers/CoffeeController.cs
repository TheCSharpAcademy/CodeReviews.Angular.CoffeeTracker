using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeTracker.UgniusFalze.Models;

namespace CoffeeTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeRecordContext _context;

        public CoffeeController(CoffeeRecordContext context)
        {
            _context = context;
        }

        // GET: api/Coffee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeeRecord>>> GetCoffeeRecords()
        {
            return await _context.CoffeeRecords.ToListAsync();
        }

        // GET: api/Coffee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeRecord>> GetCoffeeRecord(int id)
        {
            var coffeeRecord = await _context.CoffeeRecords.FindAsync(id);

            if (coffeeRecord == null)
            {
                return NotFound();
            }

            return coffeeRecord;
        }

        // PUT: api/Coffee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffeeRecord(int id, CoffeeRecord coffeeRecord)
        {
            if (id != coffeeRecord.CoffeeRecordId)
            {
                return BadRequest();
            }

            _context.Entry(coffeeRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoffeeRecordExists(id))
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

        // POST: api/Coffee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoffeeRecord>> PostCoffeeRecord(CoffeeRecord coffeeRecord)
        {
            _context.CoffeeRecords.Add(coffeeRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoffeeRecord", new { id = coffeeRecord.CoffeeRecordId }, coffeeRecord);
        }

        // DELETE: api/Coffee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffeeRecord(int id)
        {
            var coffeeRecord = await _context.CoffeeRecords.FindAsync(id);
            if (coffeeRecord == null)
            {
                return NotFound();
            }

            _context.CoffeeRecords.Remove(coffeeRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoffeeRecordExists(int id)
        {
            return _context.CoffeeRecords.Any(e => e.CoffeeRecordId == id);
        }
    }
}
