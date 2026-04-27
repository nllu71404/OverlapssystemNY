using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;
using OverlapssystemShared;

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
        public async Task<IActionResult> SaveNewDepartmentTaskAsync([FromBody] AddDepartmentTaskDTO departmentTaskDTO)
        {
            var mappedModel = MapToAddDepartmentTaskModel(departmentTaskDTO);
            var result = await _departmentTaskService.SaveNewDepartmentTaskAsync(mappedModel);
            return Handle(result);


        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentTaskAsync(int id, [FromBody] UpdateDepartmentTaskDTO departmentTaskDTO)
        {
            var mappedModel = MapToUpdateDepartmentTaskModel(departmentTaskDTO);
            var result = await _departmentTaskService.UpdateDepartmentTaskAsync(mappedModel);
            return Handle(result);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentTaskAsync(int id)
        {
            var result = await _departmentTaskService.DeleteDepartmentTaskAsync(id);
            return Handle(result);
        }


        // ----- Mapping -------

        private DepartmentTaskModel MapToAddDepartmentTaskModel(AddDepartmentTaskDTO dto)
        {
            return new DepartmentTaskModel
            {
                DepartmentID = dto.DepartmentID,
                DepartmentTaskTopic = dto.DepartmentTaskTopic,
                EmployeeName = dto.EmployeeName,
                ShiftType = dto.ShiftType
            };
        }

        private DepartmentTaskModel MapToUpdateDepartmentTaskModel(UpdateDepartmentTaskDTO dto)
        {
            return new DepartmentTaskModel
            {
                DepartmentTaskID = dto.DepartmentTaskID,
                DepartmentTaskTopic = dto.DepartmentTaskTopic,
                EmployeeName = dto.EmployeeName,
                ShiftType = dto.ShiftType
            };
        }
    }
}
