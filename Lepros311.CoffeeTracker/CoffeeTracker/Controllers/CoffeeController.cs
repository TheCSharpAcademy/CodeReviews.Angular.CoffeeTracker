using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Responses;
using CoffeeTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTracker.Api.Controllers
{
    [Route("api/coffees")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<CoffeeDto>>>> GetPagedCoffees([FromQuery] PaginationParams paginationParams)
        {
            var responseWithDtos = await _coffeeService.GetPagedCoffees(paginationParams);

            if (responseWithDtos.Status == ResponseStatus.Fail)
            {
                return BadRequest(responseWithDtos.Message);
            }

            return Ok(responseWithDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeDto>> GetCoffeeById(int id)
        {
            var response = await _coffeeService.GetCoffeeById(id);

            if (response.Status == ResponseStatus.Fail)
            {
                return NotFound(response.Message);
            }

            var returnedCoffee = response.Data;

            var coffeeDto = new CoffeeDto
            {
                Id = returnedCoffee.Id,
                Name = returnedCoffee.Name,
                Price = returnedCoffee.Price.ToString()
            };

            return Ok(coffeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<CoffeeDto>> CreateCategory([FromBody] CoffeeDto writeCoffeeDto)
        {
            var responseWithDataDto = await _coffeeService.CreateCoffee(writeCoffeeDto);

            if (responseWithDataDto.Message == "Coffee not found.")
            {
                return NotFound(responseWithDataDto.Message);
            }

            if (responseWithDataDto.Status == ResponseStatus.Fail)
            {
                return BadRequest(responseWithDataDto.Message);
            }

            return CreatedAtAction(nameof(GetCoffeeById),
                new { id = responseWithDataDto.Data.Id }, responseWithDataDto.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CoffeeDto>> UpdateCoffee(int id, [FromBody] CoffeeDto CoffeeDto)
        {
            var response = await _coffeeService.UpdateCoffee(id, CoffeeDto);

            if (response.Message == "Coffee not found.")
            {
                return NotFound(response.Message);
            }

            if (response.Status == ResponseStatus.Fail)
            {
                return BadRequest(response.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffee(int id)
        {
            var response = await _coffeeService.DeleteCoffee(id);

            if (response.Message == "Coffee not found.")
            {
                return NotFound(response.Message);
            }
            else if (response.Message == "Cannot delete coffees included in recorded sales.")
            {
                return Conflict(response.Message);
            }
            else if (response.Status == ResponseStatus.Fail)
            {
                return BadRequest(response.Message);
            }

            return NoContent();
        }

    }

}