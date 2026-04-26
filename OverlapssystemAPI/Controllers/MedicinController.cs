using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinController : ApiControllerBase
    {
        private readonly IMedicinService _medicinServices;

        public MedicinController(IMedicinService medicinServices)
        {
            _medicinServices = medicinServices;
        }
        
        //Hent
        [HttpGet("HentMedicinForBorger/{residentId}")]
        public async Task<IActionResult> GetMedicinByResidentId(int residentId)
        {
            var medicin = await _medicinServices.GetMedicinByResidentIdAsync(residentId);
            return Handle(medicin);
        }

        // Tilføjer medicintid 
        [HttpPost("TilføjMedicin")]
        public async Task<IActionResult> AddMedicinTime([FromBody] AddMedicinTimeDTO medicinDTO)
        {
            var medicinTime = new MedicinModel
            {
                ResidentID = medicinDTO.ResidentId,
                MedicinTime = medicinDTO.MedicinTime,
                IsChecked = false,
            };

            var time = await _medicinServices.AddMedicinTimeAsync(medicinTime);

            if(!time.Success)
           return Handle(time);

            return Created($"/api/Medicin/{time.Value}", time.Value);
        }

        //Delete
        [HttpDelete("{medicinTimeId}")]
        public async Task<IActionResult> DeleteMedicin(int medicinTimeId)
        {
            await _medicinServices.DeleteMedicinAsync(medicinTimeId);
            return Ok(medicinTimeId);
        }

        //Update
        [HttpPut("{medicinTimeId}")]
        public async Task<IActionResult> UpdateMedicin(int medicinTimeId, [FromBody] MedicinModel medicinModel)
        {
            await _medicinServices.UpdateMedicinAsync(medicinModel);
            return Ok(medicinTimeId);
        }


        //Put: api/MedicinTid
        [HttpPut("SetChecked/{id}")]
        public async Task<IActionResult> SetMedicinChecked(int id, [FromBody] AddMedicinTimeDTO medicinDTO)
        {
            await _medicinServices.SetMedicinCheckedAsync(id, medicinDTO.IsChecked);
            return Ok();
        }



    }
}
