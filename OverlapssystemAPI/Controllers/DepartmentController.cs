using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        //Hent Alle
        [HttpGet("HentAlleDepartments")]
        public async Task<ActionResult> GetAllDepartments()
        {
            var department = await _departmentService.GetAllDepartmentsAsync();
            return Ok(department);
        }

        //Hent by ID
        [HttpGet("HentAlleDepartmentsByID")]
        public async Task<ActionResult> GetDepartmentById(int departmentId)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(departmentId);
            return Ok(department);
        }

        //Hent by name
        [HttpGet("HentAlleDepartmentsByName")]
        public async Task<ActionResult> GetDepartmentByName(string departmentName)
        {
            var department = await _departmentService.GetDepartmentByNameAsync(departmentName);
            return Ok(department);
        }

        //Tilføj
        [HttpPost("TilføjAfdeling")]
        public async Task<ActionResult> SaveNewDepartment([FromBody] DepartmentModel departmentModel)
        {
            await _departmentService.SaveNewDepartmentAsync(departmentModel);
            return Ok(departmentModel);
        }

        //Slet
        [HttpDelete("{departmentId}")]
        public async Task<ActionResult> DeleteDepartment(int departmentId)
        {
            await _departmentService.DeleteDepartmentAsync(departmentId);
            return Ok(departmentId);
        }

        //Update
        [HttpPut("{departmentId}")]
        public async Task<ActionResult> UpdateDepartment(int departmentId, [FromBody] DepartmentModel departmentModel)
        {
            await _departmentService.UpdateDepartmentAsync(departmentModel);
            return Ok(departmentId);
        }
    }
}
