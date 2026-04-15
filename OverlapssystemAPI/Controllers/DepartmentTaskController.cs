using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentTaskController : ControllerBase
    {
        private readonly IDepartmentTaskService _departmentTaskService;
        public DepartmentTaskController(IDepartmentTaskService departmentTaskService)
        {
            _departmentTaskService = departmentTaskService;
        }

        //Hent alle
        [HttpGet("HentDepartmentTasks")]
        public async Task<ActionResult> GetAllDepartmentTasksAsync()
        {
            var departmentTasks = await _departmentTaskService.GetAllDepartmentTasksAsync();
            return Ok(departmentTasks);
        }

        //Hent departmentTask på id
        [HttpGet("HentDepartmentTasksID/{departmentTaskId}")]
        public async Task<ActionResult> GetDepartmentTaskByIdAsync(int departmentTaskId)
        {
            var departmentTasks = await _departmentTaskService.GetDepartmentTaskByIdAsync(departmentTaskId);
            return Ok(departmentTasks);
        }

        //Tilføj
        [HttpPut("TilføjDepartmentTask")]
        public async Task<ActionResult<DepartmentTaskModel>> SaveNewDepartmentTaskAsync([FromBody] DepartmentTaskModel departmentTaskModel)
        {
            await _departmentTaskService.SaveNewDepartmentTaskAsync(departmentTaskModel);
            return Ok(departmentTaskModel);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentTaskModel>> UpdateDepartmentTaskAsync(int id,[FromBody] DepartmentTaskModel departmentTaskModel)
        {
            await _departmentTaskService.UpdateDepartmentTaskAsync(departmentTaskModel);
            return Ok(departmentTaskModel);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartmentTaskAsync(int id)
        {
            await _departmentTaskService.DeleteDepartmentTaskAsync(id);
            return Ok(id);
        }
    }
}
