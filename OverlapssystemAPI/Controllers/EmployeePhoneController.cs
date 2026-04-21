using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Services;
using OverlapssytemApplication.Interfaces;


namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeePhoneController : ControllerBase
    {
        private readonly IEmployeePhoneService _employeePhoneService;

        public EmployeePhoneController(IEmployeePhoneService employeePhoneService)
        {
            _employeePhoneService = employeePhoneService;
        }

        //Hent alle medarbejdertelefonnumre
        [HttpGet("HentAlleEmployeePhones")]
        public async Task<ActionResult> GetAllEmployeePhoneNumbersAsync()
        {
            var employeePhones = await _employeePhoneService.GetAllEmployeePhoneNumbersAsync();
            return Ok(employeePhones.Value);
        }

        //Hent medarbejdertelefon på id
        [HttpGet("HentEmployeePhoneById/{employeePhoneId}")]
        public async Task<ActionResult> GetEmployeePhoneByIdAsync(int employeePhoneId)
        {
            var employeePhone = await _employeePhoneService.GetEmployeePhoneByIdAsync(employeePhoneId);
            if (employeePhone == null)
            {
                return NotFound();
            }
            return Ok(employeePhone.Value);
        }

        //Tilføj/Gem et medarbejdertelefonnummer
        [HttpPost("TilføjEmployeePhone")]
        public async Task<ActionResult> SaveNewEmployeePhoneAsync([FromBody] EmployeePhoneModel employeePhoneModel)
        {
            var result = await _employeePhoneService.SaveNewEmployeePhoneAsync(employeePhoneModel);
            return Ok(result.Value);
        }

        //Update et medarbejdertelefonnummer
        [HttpPut("OpdaterEmployeePhone/{employeePhoneId}")]
        public async Task<ActionResult> UpdateEmployeePhoneAsync(int employeePhoneId, [FromBody] EmployeePhoneModel employeePhoneModel)
        {
            await _employeePhoneService.UpdateEmployeePhoneAsync(employeePhoneModel);
            return Ok(employeePhoneModel);
        }

        //Delete et medarbejdertelefonnummer
        [HttpDelete("SletEmployeePhone/{employeePhoneId}")]
        public async Task<ActionResult> DeleteEmployeePhoneAsync(int employeePhoneId)
        {
            await _employeePhoneService.DeleteEmployeePhoneAsync(employeePhoneId);
            return Ok(employeePhoneId);
        }


    }
}
