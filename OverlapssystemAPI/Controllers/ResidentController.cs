using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemShared;

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
            return Handle(result);
        }



        // Tilføj
        [HttpPost("OpretResident")]
        public async Task<IActionResult> CreateResident([FromBody] AddResidentDTO resident)
        {
            var residentModel = MapToResidentModel(resident);
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
            return Handle(result);
        }
    

    // ---- Mapping helpers -----

     private ResidentModel MapToResidentModel(AddResidentDTO dto)
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

        private ResidentModel MapToUpdateResidentModel(UpdateResidentDTO dto, int id)
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

    }
}

