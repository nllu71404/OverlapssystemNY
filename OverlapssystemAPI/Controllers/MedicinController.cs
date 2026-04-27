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
           var mappedModel = MapToAddMedicinModel(medicinDTO);
           var id = await _medicinServices.AddMedicinTimeAsync(mappedModel);
           return Handle(id);

            
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
        public async Task<IActionResult> UpdateMedicin(int medicinTimeId, [FromBody] UpdateMedicinTimeDTO medicinDTO)
        {
            var mappedModel = MapToUpdateMedicinModel(medicinDTO);
            await _medicinServices.UpdateMedicinAsync(mappedModel);
            return Ok(medicinTimeId);
        }


        //Put: api/MedicinTid
        [HttpPut("SetChecked/{id}")]
        public async Task<IActionResult> SetMedicinChecked(int id, [FromBody] SetMedicinCheckedDTO medicinDTO)
        {
            
            await _medicinServices.SetMedicinCheckedAsync(id, medicinDTO.IsChecked);
            return Ok();
        }



    

    // ---- Mapping ----

        private MedicinModel MapToUpdateMedicinModel(UpdateMedicinTimeDTO medicinDTO)
        {
            return new MedicinModel
            {
                MedicinTimeID = medicinDTO.MedicinTimeID,
                MedicinTime = medicinDTO.MedicinTime,
                IsChecked = medicinDTO.IsChecked,
                MedicinCheckTimeStamp = medicinDTO.MedicinCheckTimeStamp
            };
        }

        private MedicinModel MapToAddMedicinModel(AddMedicinTimeDTO medicinDTO)
        {
            return new MedicinModel
            {
                ResidentID = medicinDTO.ResidentId,
                MedicinTime = medicinDTO.MedicinTime,
                IsChecked = medicinDTO.IsChecked,

            };
        }

       
    }
}
