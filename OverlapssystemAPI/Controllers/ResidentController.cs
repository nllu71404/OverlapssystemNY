using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemShared;
using OverlapssytemApplication.Common;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentController : ApiControllerBase
    {
        private readonly IResidentServices _residentServices;

        public ResidentController(IResidentServices residentServices)
        {
            _residentServices = residentServices;
        }

        // Hent alle
        [HttpGet("HenterResident")]
        public async Task<IActionResult> GetResidents()
        {
            var result = await _residentServices.LoadResidentsAsync();

            if (!result.Success)
            {
                return Handle(result);
            }
            var residentDTOs = result.Value.Select(MapToGetResidentDTO).ToList();
            return Handle(Result.Ok(residentDTOs));

        }



        // Tilføj
        [HttpPost("OpretResident")]
        public async Task<IActionResult> CreateResident([FromBody] AddResidentDTO resident)
        {
            var residentModel = MapToAddResidentModel(resident);
            var result = await _residentServices.CreateResidentAsync(residentModel);

            return Handle(result);

        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResident(int id, [FromBody] UpdateResidentDTO resident)
        {
            var residentModel = MapToUpdateResidentModel(resident, id);
            var result = await _residentServices.UpdateResidentAsync(residentModel);
            return Handle(result);
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResident(int id)
        {
            var result = await _residentServices.DeleteResidentAsync(id);
            return Handle(result);
        }

        // Hent på afdeling
        [HttpGet("Department/{id}")]
        public async Task<IActionResult> GetByDepartment(int id)
        {

            var result = await _residentServices.LoadResidentsByDepartmentAsync(id);

            if (!result.Success)
            {
                return Handle(result);
            }

            var residentDTOs = result.Value.Select(MapToGetResidentDTO).ToList();
            return Handle(Result.Ok(residentDTOs));
        }


        // ---- Mapping helpers -----

        private static ResidentModel MapToAddResidentModel(AddResidentDTO dto)
        {
            return new ResidentModel
            {
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                Status = dto.Status,
                Activity = dto.Activity,
                Family = dto.Family,
                ResidentEmployee = dto.ResidentEmployee,
                Risiko = dto.Risiko,
                Mood = dto.Mood
            };
        }

        private static ResidentModel MapToUpdateResidentModel(UpdateResidentDTO dto, int id)
        {
            return new ResidentModel
            {
                ResidentId = id,
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                Status = dto.Status,
                Activity = dto.Activity,
                Family = dto.Family,
                ResidentEmployee = dto.ResidentEmployee,
                Risiko = dto.Risiko,
                Mood = dto.Mood
            };
        }

        private static ResidentDTO MapToGetResidentDTO(ResidentModel model)
        {
            return new ResidentDTO
            {
                ResidentId = model.ResidentId,
                Name = model.Name,
                DepartmentId = model.DepartmentId,
                Status = model.Status,
                Activity = model.Activity,
                Family = model.Family,
                ResidentEmployee = model.ResidentEmployee,
                Risiko = model.Risiko,
                Mood = model.Mood,

                MedicinTimes = model.MedicinTimes
            .Select(m => new MedicinTimeDTO
            {
                MedicinTimeID = m.MedicinTimeID,
                ResidentID = m.ResidentID,
                MedicinTime = m.MedicinTime,
                IsChecked = m.IsChecked,
                MedicinCheckTimeStamp = m.MedicinCheckTimeStamp
            })
            .ToList(),

                PNMedicin = model.PNMedicin
            .Select(p => new PNMedicinDTO
            {
                PNMedicinID = p.PNMedicinID,
                ResidentID = p.ResidentID,
                PNTime = p.PNTime,
                //PNTime = p.PNTimeStamp,
                Reason = p.Reason
            })
            .ToList(),

                Shopping = model.Shopping
            .Select(s => new UpdateShoppingDTO
            {
                ShoppingID = s.ShoppingID,
                ResidentID = s.ResidentID,
                Day = s.Day,
                Time = s.Time,
                PaymentMethod = s.PaymentMethod
            })
            .ToList(),

                SpecialEvents = model.SpecialEvents
            .Select(se => new UpdateSpecialEventDTO
            {
                SpecialEventID = se.SpecialEventID,
                ResidentID = se.ResidentID,
                SpecialEventNote = se.SpecialEventNote,
                SpecialEventDateTime = se.SpecialEventDateTime
            })
            .ToList()
            };

        }
    }
}

