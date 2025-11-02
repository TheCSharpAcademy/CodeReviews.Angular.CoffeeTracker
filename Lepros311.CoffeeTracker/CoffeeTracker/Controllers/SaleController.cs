using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Responses;
using CoffeeTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTracker.Api.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<SaleDto>>>> GetPagedSales([FromQuery] PaginationParams paginationParams)
        {
            var responseWithDtos = await _saleService.GetPagedSales(paginationParams);

            if (responseWithDtos.Status == ResponseStatus.Fail)
            {
                return BadRequest(responseWithDtos.Message);
            }

            return Ok(responseWithDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSaleById(int id)
        {
            var response = await _saleService.GetSaleById(id);

            if (response.Status == ResponseStatus.Fail)
            {
                return NotFound(response.Message);
            }

            var returnedSale = response.Data;

            var saleDto = new SaleDto
            {
                Id = returnedSale.Id,
                DateAndTimeOfSale = returnedSale.DateAndTimeOfSale.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                CoffeeName = returnedSale.CoffeeName,
                CoffeeId = returnedSale.CoffeeId,
                Total = returnedSale.Total.ToString()
            };

            return Ok(saleDto);
        }

        [HttpPost]
        public async Task<ActionResult<SaleDto>> CreateSale([FromBody] CreateSaleDto writeSaleDto)
        {
            var responseWithDataDto = await _saleService.CreateSale(writeSaleDto);

            if (responseWithDataDto.Message == "Coffee not found.")
            {
                return NotFound(responseWithDataDto.Message);
            }

            if (responseWithDataDto.Status == ResponseStatus.Fail)
            {
                return BadRequest(responseWithDataDto.Message);
            }

            return CreatedAtAction(nameof(GetSaleById),
                new { id = responseWithDataDto.Data.Id }, responseWithDataDto.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SaleDto>> UpdateSale(int id, [FromBody] UpdateSaleDto updateSaleDto)
        {
            var response = await _saleService.UpdateSale(id, updateSaleDto);

            if (response.Message == "Sale not found." || response.Message == "Coffee not found.")
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
        public async Task<IActionResult> DeleteSale(int id)
        {
            var response = await _saleService.DeleteSale(id);

            if (response.Message == "Sale not found.")
            {
                return NotFound(response.Message);
            }
            else if (response.Status == ResponseStatus.Fail)
            {
                return BadRequest(response.Message);
            }

            return NoContent();
        }

    }

}