using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;



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
        [Authorize(Roles = "Administrator,Medarbejder,Simpel")]
        public async Task<IActionResult> GetAllEmployeePhoneNumbersAsync()
        {
            var result = await _employeePhoneService.GetAllEmployeePhoneNumbersAsync();

            if (!result.Success) 
            {
                 return Handle(result);
            }

            var employeePhoneDTOs = result.Value.Select(MapToGetEmployeePhoneDTO).ToList();
            return Handle(Result.Ok(employeePhoneDTOs));
        }

        //Hent medarbejdertelefon på id
        [HttpGet("HentEmployeePhoneById/{employeePhoneId}")]
        [Authorize(Roles = "Administrator,Medarbejder,Simpel")]
        public async Task<IActionResult> GetEmployeePhoneByIdAsync(int employeePhoneId)
        {
            var result = await _employeePhoneService.GetEmployeePhoneByIdAsync(employeePhoneId);

            if (!result.Success) 
            {
                 return Handle(result);
            }

            var employeePhoneDTO = MapToGetEmployeePhoneDTO(result.Value);
            return Handle(Result.Ok(employeePhoneDTO));
        }

        //Hent medarbejdertelefoner på departmentId
        [HttpGet("HentEmployeePhonesByDepartmentId/{departmentId}")]
        [Authorize(Roles = "Administrator,Medarbejder,Simpel")]
        public async Task<IActionResult> GetEmployeePhonesByDepartmentIdAsync(int departmentId)
        {
            var result = await _employeePhoneService.GetEmployeePhonesByDepartmentIdAsync(departmentId);

            if (!result.Success)
                return Handle(result);

            var employeePhoneDTOs = result.Value.Select(MapToGetEmployeePhoneDTO).ToList();
            return Handle(Result.Ok(employeePhoneDTOs));
        }

        //Tilføj/Gem et medarbejdertelefonnummer
        [HttpPost("TilføjEmployeePhone")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveNewEmployeePhoneAsync([FromBody] AddEmployeePhoneDTO employeePhoneDTO)
        {
            var employeePhoneModel = MapToAddEmployeePhoneModel(employeePhoneDTO);
            var result = await _employeePhoneService.SaveNewEmployeePhoneAsync(employeePhoneModel);
            return Handle(result);
        }

        //Update et medarbejdertelefonnummer
        [HttpPut("OpdaterEmployeePhone/{employeePhoneId}")]
        [Authorize(Roles = "Administrator,Medarbejder")]
        public async Task<IActionResult> UpdateEmployeePhoneAsync(int employeePhoneId, [FromBody] EmployeePhoneDTO employeePhoneDTO)
        {
            var employeePhoneModel = MapToUpdateEmployeePhoneModel(employeePhoneDTO);
            var result = await _employeePhoneService.UpdateEmployeePhoneAsync(employeePhoneModel);
            return Handle(result);
        }

        //Delete et medarbejdertelefonnummer
        [HttpDelete("SletEmployeePhone/{employeePhoneId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteEmployeePhoneAsync(int employeePhoneId)
        {
            var result = await _employeePhoneService.DeleteEmployeePhoneAsync(employeePhoneId);
            return Handle(result);
        }

        // ----- Mapping -----

           
       private EmployeePhoneModel MapToAddEmployeePhoneModel(AddEmployeePhoneDTO dto)
        {
            return new EmployeePhoneModel
            {
                DepartmentID = dto.DepartmentID,
                EmployeeName = dto.EmployeeName,
                PhoneNumber = dto.PhoneNumber, 
                Test = dto.Test
            };
        }

        private EmployeePhoneModel MapToUpdateEmployeePhoneModel(EmployeePhoneDTO dto)
        {
            return new EmployeePhoneModel
            {
                EmployeePhoneID = dto.EmployeePhoneID,
                DepartmentID = dto.DepartmentID,
                EmployeeName = dto.EmployeeName,
                PhoneNumber = dto.PhoneNumber, 
                Test = dto.Test
            };
        }

        private EmployeePhoneDTO MapToGetEmployeePhoneDTO(EmployeePhoneModel model)
            {
                return new EmployeePhoneDTO
                {
                    EmployeePhoneID = model.EmployeePhoneID,
                    DepartmentID = model.DepartmentID,
                    EmployeeName = model.EmployeeName,
                    PhoneNumber = model.PhoneNumber, 
                    Test = model.Test
                };
        }
    }
}
