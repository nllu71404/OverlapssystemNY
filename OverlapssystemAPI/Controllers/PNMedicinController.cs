using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PNMedicinController : ControllerBase
    {
        private readonly IPNMedicinService _pNMedicinService;

        public PNMedicinController (IPNMedicinService pnMedicinService)
        {
            _pNMedicinService = pnMedicinService;
        }

        //Hent
        [HttpGet("PNMedicintider/{residentId}")]
        public async Task<ActionResult> GetPNMedicinByResidentIdAsync(int residentId)
        {
            var pnmedicin = await _pNMedicinService.GetPNMedicinByResidentIdAsync(residentId);
            return Ok(pnmedicin);
        }

        //Tilføj
        [HttpPost("TilføjPNMedicin")]
        public async Task<ActionResult<int>> AddPNMedicinTime([FromBody] AddPNMedicinDTO addPNMedicinDTO)
        {
            var id = await _pNMedicinService.AddPNMedicinAsync(addPNMedicinDTO.ResidentID, addPNMedicinDTO.PNTime, addPNMedicinDTO.Reason);
            return Ok(id);
        }

        //Update
        [HttpPut("{pNMedicinId}")]
        public async Task<ActionResult<PNMedicinModel>> UpdatePNMedicinAsync(int pNMedicinId, [FromBody] PNMedicinModel pNMedicin)
        {
            await _pNMedicinService.UpdatePNMedicinAsync(pNMedicin);
            return Ok(pNMedicinId);

        }
        
        //Delete
        [HttpDelete("{pnMedicinId}")]
        public async Task<ActionResult> DeletePNMedicinAsync(int pNMedicinId)
        {
            // Placeholder for updating a resident logic
            await _pNMedicinService.DeletePNMedicinAsync(pNMedicinId);
            return Ok(pNMedicinId);

        }






    }
}
