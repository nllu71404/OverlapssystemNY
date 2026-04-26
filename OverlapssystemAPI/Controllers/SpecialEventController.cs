using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialEventController : ApiControllerBase
    {
        private readonly ISpecialEventService _specialEventService;
        public SpecialEventController(ISpecialEventService specialEventService)
        {
            _specialEventService = specialEventService;
        }

        //Hent
        [HttpGet("HentSpecialEventForBorger/{residentId}")]
        public async Task<IActionResult> GetSpecialTaskByResidentID(int residentId)
        {
            var specialEvent = await _specialEventService.GetSpecialEventByResidentIdAsync(residentId);
            return Handle(specialEvent); 
        }
        //Tilføj
        [HttpPost("TilføjSpecialEvent")]
        public async Task<IActionResult> AddSpecialEvent([FromBody] AddSpecialEventDTO addSpecialEventDTO)
        {
            var specialEvent = new SpecialEventModel
            {
                ResidentID = addSpecialEventDTO.ResidentID,
                SpecialEventNote = addSpecialEventDTO.SpecialEventNote,
                SpecialEventDateTime = addSpecialEventDTO.SpecialEventDateTime,
            };

            var specialId = await _specialEventService.SaveNewSpecialEventAsync(specialEvent);
            
            if (!specialId.Success) 
            return Handle(specialId); //Fejlhåndtering

            //Hvis succes, returneres 201 Created med lokationen for den nye ressource
            return Created($"/api/SpecialEvent/{specialId.Value}", specialId.Value);
        }

        //Update
        [HttpPut("{specialEventID}")]
        public async Task<IActionResult> UpdateSpecialTask(int specialEventID, [FromBody] SpecialEventModel specialEventModel)
        {
           var result = await _specialEventService.UpdateSpecialEventAsync(specialEventModel);
            return Handle(result);
        }

        //Delete
        [HttpDelete("{specialEventID}")]
        public async Task<IActionResult> DeleteSpecialEvent(int specialEventID)
        {
            var result = await _specialEventService.DeleteSpecialEventAsync(specialEventID);
            return Handle(result);
        }
    }
}
