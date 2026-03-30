using Microsoft.AspNetCore.Mvc;
using OverlapssystemInfrastructure.Repositories;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentRepository _residentRepository;
        public ResidentController(IResidentRepository residentRepository)
        {
            _residentRepository = residentRepository;
        }
        private ResidentModel _residents = new ResidentModel();


        // GET: api/resident
        [HttpGet("OpretBorger")]
        public async Task<ActionResult> GetResidents()
        {
            // Placeholder for getting residents logic
            return Ok(_residents);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ResidentModel>> CreateResident(int id, [FromBody] ResidentModel resident)
        {
            // Placeholder for creating a resident logic
            return Ok("Resident created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResidentModel>> UpdateResident(int id, [FromBody] ResidentModel resident)
        {
            // Placeholder for updating a resident logic
            return Ok("Resident updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResident(int id) 
        {
            // Placeholder for updating a resident logic
            return Ok("Resident deleted successfully.");
        }


    }
}
