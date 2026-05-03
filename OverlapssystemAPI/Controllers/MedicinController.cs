using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;
using OverlapssytemApplication.Common;

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
            var result = await _medicinServices.GetMedicinByResidentIdAsync(residentId);

            if (!result.Success)
            {
                return Handle(result);
            }

            var medicinDTOs = result.Value.Select(MapToGetMedicinTimeDTO).ToList();
            return Handle(Result.Ok(medicinDTOs));
        }

        // Tilføjer medicintid 
        [HttpPost("TilføjMedicin")]
        public async Task<IActionResult> AddMedicinTime([FromBody] AddMedicinTimeDTO medicinDTO)
        {
            var mappedModel = MapToAddMedicinModel(medicinDTO);
            var id = await _medicinServices.CreateMedicinTimeAsync(mappedModel);
            return Handle(id);


        }

        //Delete
        [HttpDelete("{medicinTimeId}")]
        public async Task<IActionResult> DeleteMedicin(int medicinTimeId)
        {
            var result = await _medicinServices.DeleteMedicinAsync(medicinTimeId);
            return Handle(result);
        }

        //Update
        [HttpPut("{medicinTimeId}")]
        public async Task<IActionResult> UpdateMedicin(int medicinTimeId, [FromBody] UpdateMedicinTimeDTO medicinDTO)
        {
            var mappedModel = MapToUpdateMedicinModel(medicinDTO);
            var result = await _medicinServices.UpdateMedicinAsync(mappedModel);
            return Handle(result);
        }


        //Put: api/MedicinTid
        [HttpPut("SetChecked/{id}")]
        public async Task<IActionResult> SetMedicinChecked(int id, [FromBody] SetMedicinCheckedDTO medicinDTO)
        {

            var result = await _medicinServices.SetMedicinCheckedAsync(id, medicinDTO.IsChecked);
            return Handle(result);
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
                ResidentID = medicinDTO.ResidentID,
                MedicinTime = medicinDTO.MedicinTime,
                IsChecked = medicinDTO.IsChecked,

            };
        }

        private MedicinTimeDTO MapToGetMedicinTimeDTO(MedicinModel medicinModel)
        {
            return new MedicinTimeDTO
            {
                MedicinTimeID = medicinModel.MedicinTimeID,
                ResidentID = medicinModel.ResidentID,
                MedicinTime = medicinModel.MedicinTime,
                IsChecked = medicinModel.IsChecked,
                MedicinCheckTimeStamp = medicinModel.MedicinCheckTimeStamp
            };


        }
    }
}
