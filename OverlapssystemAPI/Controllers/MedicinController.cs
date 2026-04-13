using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinController : ControllerBase
    {
        private readonly IMedicinService _medicinServices;

        public MedicinController(IMedicinService medicinServices)
        {
            _medicinServices = medicinServices;
        }
        
        //Hent
        [HttpGet("HentMedicinForBorger/{residentId}")]
        public async Task<ActionResult> GetMedicinByResidentId(int residentId)
        {
            var medicin = await _medicinServices.GetMedicinByResidentIdAsync(residentId);
            return Ok(medicin);
        }

        // Tilføjer medicintid 
        [HttpPost("TilføjMedicin")]
        public async Task<ActionResult<int>> AddMedicinTime([FromBody] AddMedicinTimeDTO medicinDTO)
        {
            var id = await _medicinServices.AddMedicinTimeAsync(medicinDTO.ResidentId, medicinDTO.DateTime);
            return Ok(id);
        }

        //Delete
        [HttpDelete("{medicinTimeId}")]
        public async Task<ActionResult> DeleteMedicin(int medicinTimeId)
        {
            await _medicinServices.DeleteMedicinAsync(medicinTimeId);
            return Ok(medicinTimeId);
        }

        //Update
        [HttpPut("{medicinTimeId}")]
        public async Task<ActionResult> UpdateMedicin(int medicinTimeId, [FromBody] MedicinModel medicinModel)
        {
            await _medicinServices.UpdateMedicinAsync(medicinModel);
            return Ok(medicinTimeId);
        }


        //Put: api/MedicinTid
        [HttpPut("SetChecked/{id}")]
        public async Task<IActionResult> SetMedicinChecked(int id, [FromBody] SetMedicinCheckedDTO dto)
        {
            await _medicinServices.SetMedicinCheckedAsync(id, dto.IsChecked);
            return Ok();
        }



    }
}
