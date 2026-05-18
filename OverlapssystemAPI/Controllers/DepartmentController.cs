using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Interfaces;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ApiControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        //Hent Alle
        [HttpGet("HentAlleDepartments")]
        [Authorize(Roles = "Administrator,Medarbejder,Simpel")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();

            if (!result.Success)
            {
                return Handle(result);
            }

            var departmentDTOs = result.Value.Select(MapToGetDepartmentDTO).ToList();
            return Handle(Result.Ok(departmentDTOs));
        }

        //Hent by ID
        [HttpGet("HentAlleDepartmentsByID/{departmentId}")]
        [Authorize(Roles = "Administrator,Medarbejder,Simpel")]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(departmentId);
            if (!result.Success) 
            {
                Handle(result);
            }

            var departmentDTO = MapToGetDepartmentDTO(result.Value);

            return Handle(Result.Ok(departmentDTO));
        }

        //Hent by name
        [HttpGet("HentAlleDepartmentsByName/{departmentName}")]
        [Authorize(Roles = "Administrator,Medarbejder,Simpel")]
        public async Task<IActionResult> GetDepartmentByName(string departmentName)
        {
           
            var result = await _departmentService.GetDepartmentByNameAsync(departmentName);

            if (!result.Success)
            {
                return Handle(result);
            }

            var departmentDTO = MapToGetDepartmentDTO(result.Value);
            return Handle(Result.Ok(departmentDTO));
        }

        //Tilføj
        [HttpPost("TilføjAfdeling")]
        public async Task<IActionResult> SaveNewDepartment([FromBody] AddDepartmentDTO departmentDTO)
        {
            var departmentModel = MapToAddDepartmentModel(departmentDTO);
            var result = await _departmentService.SaveNewDepartmentAsync(departmentModel);
            return Handle(result);


        }

        //Slet
        [HttpDelete("{departmentId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            var result = await _departmentService.DeleteDepartmentAsync(departmentId);
            return Handle(result);
        }

        //Update
        [HttpPut("{departmentId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateDepartment(int departmentId, [FromBody] DepartmentDTO departmentDTO)
        {
            var departmentModel = MapToUpdateDepartmentModel(departmentDTO);
            var result = await _departmentService.UpdateDepartmentAsync(departmentModel);
            return Handle(result);
        }


        // ----- Mapping -------

        private DepartmentModel MapToAddDepartmentModel(AddDepartmentDTO departmentDTO)
        {
            return new DepartmentModel
            {
                Name = departmentDTO.Name

            };
        }

        private DepartmentModel MapToUpdateDepartmentModel(DepartmentDTO departmentDTO)
        {
            return new DepartmentModel
            {
                DepartmentID = departmentDTO.DepartmentID,
                Name = departmentDTO.Name
            };
        }

        private DepartmentDTO MapToGetDepartmentDTO(DepartmentModel departmentModel)
        {
            return new DepartmentDTO
            {
                DepartmentID = departmentModel.DepartmentID,
                Name = departmentModel.Name
            };
        }
    }
}
