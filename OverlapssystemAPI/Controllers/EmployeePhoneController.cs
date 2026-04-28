using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Services;
using OverlapssytemApplication.Interfaces;



namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeePhoneController : ApiControllerBase
    {
        private readonly IEmployeePhoneService _employeePhoneService;

        public EmployeePhoneController(IEmployeePhoneService employeePhoneService)
        {
            _employeePhoneService = employeePhoneService;
        }

        //Hent alle medarbejdertelefonnumre
        [HttpGet("HentAlleEmployeePhones")]
        public async Task<IActionResult> GetAllEmployeePhoneNumbersAsync()
        {
            var employeePhones = await _employeePhoneService.GetAllEmployeePhoneNumbersAsync();
            return Handle(employeePhones);
        }

        //Hent medarbejdertelefon på id
        [HttpGet("HentEmployeePhoneById/{employeePhoneId}")]
        public async Task<IActionResult> GetEmployeePhoneByIdAsync(int employeePhoneId)
        {
            var result = await _employeePhoneService.GetEmployeePhoneByIdAsync(employeePhoneId);
            return Handle(result);
        }

        //Hent medarbejdertelefoner på departmentId
        [HttpGet("HentEmployeePhonesByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetEmployeePhonesByDepartmentIdAsync(int departmentId)
        {
            var result = await _employeePhoneService.GetEmployeePhonesByDepartmentIdAsync(departmentId);
            return Handle(result);
        }

        //Tilføj/Gem et medarbejdertelefonnummer
        [HttpPost("TilføjEmployeePhone")]
        public async Task<IActionResult> SaveNewEmployeePhoneAsync([FromBody] EmployeePhoneModel employeePhoneModel)
        {
            var result = await _employeePhoneService.SaveNewEmployeePhoneAsync(employeePhoneModel);
            return Handle(result);
        }

        //Update et medarbejdertelefonnummer
        [HttpPut("OpdaterEmployeePhone/{employeePhoneId}")]
        public async Task<IActionResult> UpdateEmployeePhoneAsync(int employeePhoneId, [FromBody] EmployeePhoneModel employeePhoneModel)
        {
            var result = await _employeePhoneService.UpdateEmployeePhoneAsync(employeePhoneModel);
            return Handle(result);
        }

        //Delete et medarbejdertelefonnummer
        [HttpDelete("SletEmployeePhone/{employeePhoneId}")]
        public async Task<IActionResult> DeleteEmployeePhoneAsync(int employeePhoneId)
        {
            var result = await _employeePhoneService.DeleteEmployeePhoneAsync(employeePhoneId);
            return Handle(result);
        }


    }
}
