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
        public async Task<IActionResult> CreateResident([FromBody] ResidentModel resident)
        {
            var result = await _residentServices.CreateResidentAsync(resident);
            return Handle(result);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResident(int id, [FromBody] ResidentModel resident)
        {
            resident.ResidentId = id; // Sørger for at ID'et i URL'en bruges til at identificere hvilken beboer der skal opdateres

            var result = await _residentServices.UpdateResidentAsync(resident);
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
    }




}

