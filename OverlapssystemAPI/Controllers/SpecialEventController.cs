using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common;

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
            var result = await _specialEventService.GetSpecialEventByResidentIdAsync(residentId);

            if (!result.Success)
            {
                return Handle(result);
            }

            var specialEventDTOs = result.Value.Select(MapToGetSpecialEventDTO).ToList();
            return Handle(Result.Ok(specialEventDTOs)); 
        }
        //Tilføj
        [HttpPost("TilføjSpecialEvent")]
        public async Task<IActionResult> AddSpecialEvent([FromBody] AddSpecialEventDTO addSpecialEventDTO)
        {
           var specialEventModel = MapToAddSpecialEventModel(addSpecialEventDTO);

            var result = await _specialEventService.SaveNewSpecialEventAsync(specialEventModel);
            
         
            return Handle(result);

    
        }

        //Update
        [HttpPut("{specialEventID}")]
        public async Task<IActionResult> UpdateSpecialTask(int specialEventID, [FromBody] UpdateSpecialEventDTO specialEventDTO)
        {
            var specialEventModel = MapToUpdateSpecialEventModel(specialEventDTO);
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


        // ----- Mapping ----- 
        
        
        
        private static SpecialEventModel MapToAddSpecialEventModel(AddSpecialEventDTO addSpecialEventDTO)
        {
            return new SpecialEventModel
            {
                ResidentID = addSpecialEventDTO.ResidentID,
                SpecialEventNote = addSpecialEventDTO.SpecialEventNote,
                SpecialEventDateTime = addSpecialEventDTO.SpecialEventDateTime,
            };
        }

        private static SpecialEventModel MapToUpdateSpecialEventModel(UpdateSpecialEventDTO updateSpecialEventDTO)
        {
            return new SpecialEventModel
            {
                SpecialEventID = updateSpecialEventDTO.SpecialEventID,
                ResidentID = updateSpecialEventDTO.ResidentID,
                SpecialEventNote = updateSpecialEventDTO.SpecialEventNote,
                SpecialEventDateTime = updateSpecialEventDTO.SpecialEventDateTime,
            };
        }

        private static UpdateSpecialEventDTO MapToGetSpecialEventDTO(SpecialEventModel specialEventModel)
        {
            return new UpdateSpecialEventDTO
            {
                SpecialEventID = specialEventModel.SpecialEventID,
                ResidentID = specialEventModel.ResidentID,
                SpecialEventNote = specialEventModel.SpecialEventNote,
                SpecialEventDateTime = specialEventModel.SpecialEventDateTime,
            };
        }
    }
}
