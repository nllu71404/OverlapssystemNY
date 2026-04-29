using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common;


namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PNMedicinController : ApiControllerBase 
    {
        private readonly IPNMedicinService _pNMedicinService;

        public PNMedicinController (IPNMedicinService pnMedicinService)
        {
            _pNMedicinService = pnMedicinService;
        }

        //Hent
        [HttpGet("PNMedicintider/{residentId}")]
        public async Task<IActionResult> GetPNMedicinByResidentIdAsync(int residentId)
        {
            var result = await _pNMedicinService.GetPNMedicinByResidentIdAsync(residentId);

            if (!result.Success)
            {
                return Handle(result);
            }

            var pNMedicinDTOs = result.Value.Select(MapToGetPNMedicinDTO).ToList();
            return Handle(Result.Ok(pNMedicinDTOs));
        }

        //Tilføj
        [HttpPost("TilføjPNMedicin")]
        public async Task<IActionResult> AddPNMedicinTime([FromBody] AddPNMedicinDTO addPNMedicinDTO)
        { 
            var pNMedicinModel = MapToAddPNMedicinModel(addPNMedicinDTO);
            var result = await _pNMedicinService.AddPNMedicinAsync(pNMedicinModel);

           
                return Handle(result);
            
           
        }

        //Update
        [HttpPut("{pNMedicinId}")]
        public async Task<IActionResult> UpdatePNMedicinAsync(int pNMedicinId, [FromBody] UpdatePNMedicinDTO updatePNMedicinDTO)
        {
            var pNMedicinModel = MapToUpdatePNMedicinModel(updatePNMedicinDTO);
            var result = await _pNMedicinService.UpdatePNMedicinAsync(pNMedicinModel);
            return Handle(result);

        }
        
        //Delete
        [HttpDelete("{pnMedicinId}")]
        public async Task<IActionResult> DeletePNMedicinAsync(int pNMedicinId)
        {
            var result = await _pNMedicinService.DeletePNMedicinAsync(pNMedicinId);
            return Handle(result);
        }



        // ----- Mapping -----

            
       

        private PNMedicinModel MapToAddPNMedicinModel(AddPNMedicinDTO dto)
        {
            return new PNMedicinModel
            {
                ResidentID = dto.ResidentID,
                PNTime = dto.PNTime,
                Reason = dto.Reason
            };
        }


        private PNMedicinModel MapToUpdatePNMedicinModel(UpdatePNMedicinDTO dto)
        {
            return new PNMedicinModel
            {
                PNMedicinID = dto.PNMedicinID,
                PNTime = dto.PNTime,
                Reason = dto.Reason
            };
        }

        private PNMedicinDTO MapToGetPNMedicinDTO(PNMedicinModel model)
            {
                return new PNMedicinDTO
                {
                    PNMedicinID = model.PNMedicinID,
                    ResidentID = model.ResidentID,
                    PNTime = model.PNTime,
                    Reason = model.Reason
                };
        }


    }
}
