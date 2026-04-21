using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentTaskController : ApiControllerBase
    {
        private readonly IDepartmentTaskService _departmentTaskService;
        public DepartmentTaskController(IDepartmentTaskService departmentTaskService)
        {
            _departmentTaskService = departmentTaskService;
        }

        //Hent alle
        [HttpGet("HentDepartmentTasks")]
        public async Task<IActionResult> GetAllDepartmentTasksAsync()
        {
            var departmentTasks = await _departmentTaskService.GetAllDepartmentTasksAsync();
            return Handle(departmentTasks);
        }

        //Hent departmentTask på id
        [HttpGet("HentDepartmentTasksID/{departmentTaskId}")]
        public async Task<IActionResult> GetDepartmentTaskByIdAsync(int departmentTaskId)
        {
            var departmentTasks = await _departmentTaskService.GetDepartmentTaskByIdAsync(departmentTaskId);
            return Handle(departmentTasks);
        }

        //Hent departmentTask på departmentID
        [HttpGet("HentDepartmentTaskByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetDepartmentTasksByDepartmentIdAsync(int departmentId)
        {
            var departmentTasks = await _departmentTaskService.GetDepartmentTasksByDepartmentIdAsync(departmentId);
            return Handle(departmentTasks);
        }

        //Tilføj
        [HttpPut("TilføjDepartmentTask")]
        public async Task<IActionResult> SaveNewDepartmentTaskAsync([FromBody] DepartmentTaskModel departmentTaskModel)
        {
            var result = await _departmentTaskService.SaveNewDepartmentTaskAsync(departmentTaskModel);

            if (!result.Success)
                return Handle(result);

            return Created($"/api/DepartmentTask/{result.Value}", result.Value);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentTaskAsync(int id,[FromBody] DepartmentTaskModel departmentTaskModel)
        {
            var result = await _departmentTaskService.UpdateDepartmentTaskAsync(departmentTaskModel);
            return Handle(result);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentTaskAsync(int id)
        {
            var result = await _departmentTaskService.DeleteDepartmentTaskAsync(id);
            return Handle(result);
        }
    }
}
