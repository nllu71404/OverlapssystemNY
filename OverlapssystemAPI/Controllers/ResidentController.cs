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
        
        //Hent
        [HttpGet("HenterResident")]
        public async Task<ActionResult> GetResidents()
        {
            //Skal kalde LoadResidentAsync og returnerer resultatet
            var residents = await _residentServices.LoadResidentsAsync();
            return Ok(residents);
        }

        //Tilføj
        [HttpPost("OpretResident")]
        public async Task<ActionResult<ResidentModel>> CreateResident([FromBody] ResidentModel resident)
        {
            // Placeholder for creating a resident logic
            await _residentServices.CreateResidentAsync(resident);
            return Ok(resident);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<ResidentModel>> UpdateResident(int id, [FromBody] ResidentModel resident)
        {
            // Placeholder for updating a resident logic
            await _residentServices.UpdateResidentAsync(resident);
            return Ok(resident);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResident(int Id) 
        {
            // Placeholder for updating a resident logic
            await _residentServices.DeleteResidentAsync(Id);
            return Ok(Id);

        }
        //Hent på afdeling
        [HttpGet("Department/{id}")]
        public async Task<ActionResult> GetByDepartment(int id)
        {
            var residents = await _residentServices.LoadResidentsByDepartmentAsync(id);
            return Ok(residents);
        }

        



    }
}
