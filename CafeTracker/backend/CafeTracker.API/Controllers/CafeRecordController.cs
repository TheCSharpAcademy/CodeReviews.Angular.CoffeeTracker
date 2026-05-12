using CafeTracker.API.Data;
using CafeTracker.API.Models;
using CafeTracker.API.Models.Dtos;
using CafeTracker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CafeTracker.API.Controllers;
[ApiController]
[Route("api/cafe-records")]
public class CafeRecordController: ControllerBase
{
    private readonly ILogger<CafeRecordController> _logger;

    private readonly ICafeRecordService _cafeRecordService;
    // GET
    public CafeRecordController(ILogger<CafeRecordController> logger, ICafeRecordService cafeRecordService)
    {
        _logger = logger;
        _cafeRecordService = cafeRecordService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? drinkName, [FromQuery] DateOnly? date, [FromQuery] ProductCategory? category)
    {
        var result = await _cafeRecordService.GetCafeRecords(drinkName, date, category);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _cafeRecordService.GetCafeRecordById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateCafeRecord record)
    {
        var result = await _cafeRecordService.CreateCafeRecord(record);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCafeRecord record)
    {
        var result = await _cafeRecordService.UpdateCafeRecord(id, record);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cafeRecordService.DeleteCafeRecord(id);
        return NoContent();
    }
}