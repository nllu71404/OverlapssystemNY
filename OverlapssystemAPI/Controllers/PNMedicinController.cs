using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
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
        public async Task<ActionResult> SaveNewPNMedicinTime([FromBody] PNMedicinModel pnMedicin)
        {
            await _pNMedicinService.SaveNewPNMedicinAsync(pnMedicin);
            return Ok(pnMedicin);
        }

        //Update
        [HttpPut("{pNMedicinId}")]
        public async Task<ActionResult<PNMedicinModel>> UpdatePNMedicinAsync(int pNMedicinId, [FromBody] PNMedicinModel pNMedicin)
        {
            await _pNMedicinService.UpdatePNMedicinAsync(pNMedicin);
            return Ok(pNMedicin);

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
