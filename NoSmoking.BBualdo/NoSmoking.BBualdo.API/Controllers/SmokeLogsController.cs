using Microsoft.AspNetCore.Mvc;
using NoSmoking.BBualdo.API.Models;
using NoSmoking.BBualdo.API.Repositories;

namespace NoSmoking.BBualdo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SmokeLogsController : ControllerBase
{
  private readonly ISmokeLogsRepository _smokeLogsRepository;
  public SmokeLogsController(ISmokeLogsRepository smokeLogsRepository)
  {
    _smokeLogsRepository = smokeLogsRepository;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<SmokeLog>>> GetAllLogs()
  {
    IEnumerable<SmokeLog> logs = await _smokeLogsRepository.GetAllAsync();
    if (logs == null) return NotFound();
    return Ok(logs);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<SmokeLog>> GetLogById(int id)
  {
    SmokeLog? log = await _smokeLogsRepository.GetAsync(id);
    if (log == null) return NotFound();
    return Ok(log);
  }

  [HttpPost]
  public async Task<ActionResult> AddLog(SmokeLog log)
  {
    if (log == null) return BadRequest();

    await _smokeLogsRepository.AddAsync(log);
    return CreatedAtAction(nameof(AddLog), log);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> UpdateLog(int id, SmokeLog log)
  {
    if (log == null) return BadRequest();
    if (id != log.Id) return NotFound();
    await _smokeLogsRepository.UpdateAsync(log);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteLog(int id)
  {
    await _smokeLogsRepository.DeleteAsync(id);
    return NoContent();
  }
}