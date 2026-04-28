using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

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
            var pnmedicin = await _pNMedicinService.GetPNMedicinByResidentIdAsync(residentId);
            return Handle(pnmedicin);
        }

        //Tilføj
        [HttpPost("TilføjPNMedicin")]
        public async Task<IActionResult> AddPNMedicinTime([FromBody] AddPNMedicinDTO addPNMedicinDTO) //Denne ser anderledes ud end de andre Create!
        {
           
            var id = await _pNMedicinService.AddPNMedicinAsync(addPNMedicinDTO.ResidentID, addPNMedicinDTO.PNTime, addPNMedicinDTO.Reason);

           
                return Handle(id);
            
           
        }

        //Update
        [HttpPut("{pNMedicinId}")]
        public async Task<IActionResult> UpdatePNMedicinAsync(int pNMedicinId, [FromBody] PNMedicinModel pNMedicin)
        {
            var result = await _pNMedicinService.UpdatePNMedicinAsync(pNMedicin);
            return Handle(result);

        }
        
        //Delete
        [HttpDelete("{pnMedicinId}")]
        public async Task<IActionResult> DeletePNMedicinAsync(int pNMedicinId)
        {
            var result = await _pNMedicinService.DeletePNMedicinAsync(pNMedicinId);
            return Handle(result);
        }






    }
}
