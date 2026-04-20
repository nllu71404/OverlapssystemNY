using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialEventController : ControllerBase
    {
        private readonly ISpecialEventService _specialEventService;
        public SpecialEventController(ISpecialEventService specialEventService)
        {
            _specialEventService = specialEventService;
        }

        //Hent
        [HttpGet("HentSpecialEventForBorger/{residentId}")]
        public async Task<ActionResult> GetSpecialTaskByResidentID(int residentId)
        {
            var specialEvent = await _specialEventService.GetSpecialEventByResidentIdAsync(residentId);
            return Ok(specialEvent); 
        }
        //Tilføj
        [HttpPost("TilføjSpecialEvent")]
        public async Task<ActionResult> AddSpecialEvent([FromBody] AddSpecialEventDTO addSpecialEventDTO)
        {
            var specialEvent = new SpecialEventModel
            {
                ResidentID = addSpecialEventDTO.ResidentID,
                SpecialEventNote = addSpecialEventDTO.SpecialEventNote,
                SpecialEventDateTime = addSpecialEventDTO.SpecialEventDateTime,
            };

            var special = await _specialEventService.SaveNewSpecialEventAsync(specialEvent);
            return Ok(special);
        }

        //Update
        [HttpPut("{specialEventID}")]
        public async Task<ActionResult> UpdateSpecialTask(int specialEventID, [FromBody] SpecialEventModel specialEventModel)
        {
            await _specialEventService.UpdateSpecialEventAsync(specialEventModel);
            return Ok(specialEventModel);
        }

        //Delete
        [HttpDelete("{specialEventID}")]
        public async Task<ActionResult> DeleteSpecialEvent(int specialEventID)
        {
            await _specialEventService.DeleteSpecialEventAsync(specialEventID);
            return Ok(specialEventID);
        }
    }
}
