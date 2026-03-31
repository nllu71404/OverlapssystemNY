using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemShared;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentServices _residentServices;
        public ResidentController(IResidentServices residentServices)
        {
            _residentServices = residentServices;
        }
        


        // GET: api/resident
        [HttpGet("HenterResident")]
        public async Task<ActionResult> GetResidents()
        {
            //Skal kalde LoadResidentAsync og returnerer resultatet
            var residents = await _residentServices.LoadResidentsAsync();
            return Ok(residents);
        }

        [HttpPost("OpretResident")]
        public async Task<ActionResult<ResidentModel>> CreateResident([FromBody] ResidentModel resident)
        {
            // Placeholder for creating a resident logic
            await _residentServices.CreateResidentAsync(resident);
            return Ok(resident);
        }

        // Tjekke medicin af når den er blevet givet 
        [HttpPost("MedicinGiven")]
        public async Task<ActionResult<AddMedicinTimeDTO>> AddMedicinTime([FromBody] AddMedicinTimeDTO medicinDTO)
        {
            await _residentServices.AddMedicinTimeAsync(medicinDTO.ResidentId, medicinDTO.DateTime);
            return Ok(medicinDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResidentModel>> UpdateResident(int id, [FromBody] ResidentModel resident)
        {
            // Placeholder for updating a resident logic
            await _residentServices.UpdateResidentAsync(resident);
            return Ok(resident);
        }

        [HttpDelete("{residentid}")]
        public async Task<ActionResult> DeleteResident(int residentId) 
        {
            // Placeholder for updating a resident logic
            await _residentServices.DeleteResidentAsync(residentId);
            return Ok(residentId);

        }

        // Tilføjet så API service og frontend kan bruge denne
        [HttpGet("Department/{id}")]
        public async Task<ActionResult> GetByDepartment(int id)
        {
            var residents = await _residentServices.LoadResidentsByDepartmentAsync(id);
            return Ok(residents);
        }

        



    }
}
