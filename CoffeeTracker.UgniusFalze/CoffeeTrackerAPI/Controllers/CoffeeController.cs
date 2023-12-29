using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeTracker.UgniusFalze.Models;
using CoffeeTrackerAPI.Repositories;

namespace CoffeeTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeRepository _repository;

        public CoffeeController(ICoffeeRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeeRecord>>> GetCoffeeRecords()
        {
            return await _repository.GetRecords();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeRecord>> GetRecord(int id)
        {
            var shift = await _repository.GetCoffeeRecord(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffeeRecord(int id, CoffeeRecord coffeeRecord)
        {
            if (id != coffeeRecord.CoffeeRecordId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateCoffeeRecord(coffeeRecord);
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
        
        [HttpPost]
        public async Task<ActionResult<CoffeeRecord>> PostCoffeeRecord(CoffeeRecord coffeeRecord)
        {
            await _repository.AddCoffeeRecord(coffeeRecord);

            return CreatedAtAction(nameof(GetRecord), new { id = coffeeRecord.CoffeeRecordId }, coffeeRecord);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffeeRecord(int id)
        {
            var coffeeRecord = await _repository.GetCoffeeRecord(id);
            if (coffeeRecord == null)
            {
                return NotFound();
            }

            await _repository.DeleteCoffeeRecord(coffeeRecord);

            return NoContent();
        }

        private bool CoffeeRecordExists(int id)
        {
            return _repository.CoffeeRecordExists(id);
        }
    }
}
