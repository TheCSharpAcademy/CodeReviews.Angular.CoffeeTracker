using EnergyDrinkTracker.Forser_API.Models;
using EnergyDrinkTracker.Forser_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EnergyDrinkTracker.Forser_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private IRecordRepository RecordRepository { get; set; }

        public RecordsController(IRecordRepository recordRepository)
        {
            RecordRepository = recordRepository;
        }

        // Records Controller
        [HttpGet("GetRecords")]
        public async Task<IEnumerable<Records>> GetRecordsAsync()
        {
            return await RecordRepository.GetAllRecordsAsync();
        }
        [HttpGet("GetRecord/{id:int}")]
        public async Task<Records> GetRecordById(int id)
        {
            return await RecordRepository.GetRecordByIdAsync(id);
        }
        [HttpPost("NewRecord")]
        public async Task<ActionResult<Records>> NewRecord(Records record)
        {
            try
            {
                if (record == null)
                {
                    return BadRequest();
                }

                var createdRecord = await RecordRepository.CreateRecordAsync(record);

                return CreatedAtAction("GetRecords", new { id = createdRecord.Id }, createdRecord);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating a new Record");
            }
        }
        [HttpPut("UpdateRecord/{id:int}")]
        public async Task<ActionResult<Records>> UpdateRecord(int id, Records record)
        {
            try
            {
                if (record == null)
                {
                    return BadRequest();
                }

                var recordToUpdate = await RecordRepository.GetByIdAsync(id);

                if (recordToUpdate == null)
                {
                    return NotFound($"Record with Id = {id} not found");
                }

                record.Id = id;
                return await RecordRepository.UpdateRecordAsync(record);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating Record");
            }
        }
        [HttpDelete("DeleteRecord/{id:int}")]
        public ActionResult DeleteRecord(int id)
        {
            bool isDeleted = RecordRepository.DeleteRecordById(id);

            if (isDeleted)
            {
                return RedirectToAction("GetRecords");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the record");
            }
        }
    }
}