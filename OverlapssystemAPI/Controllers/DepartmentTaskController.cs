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
            var result = await _departmentTaskService.GetAllDepartmentTasksAsync();

            if (!result.Success)
                return Handle(result);

            var dtoList = result.Value.Select(MapToDTO).ToList();

            return Handle(Result.Ok(dtoList));
        }

        //Hent departmentTask på id
        [HttpGet("HentDepartmentTasksID/{departmentTaskId}")]
        public async Task<IActionResult> GetDepartmentTaskByIdAsync(int departmentTaskId)
        {
            var result = await _departmentTaskService.GetDepartmentTaskByIdAsync(departmentTaskId);

            if (!result.Success)
                return Handle(result);

            var dto = MapToDTO(result.Value);

            return Handle(Result.Ok(dto));
        }

        //Hent departmentTask på departmentID
        [HttpGet("HentDepartmentTaskByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetDepartmentTasksByDepartmentIdAsync(int departmentId)
        {
            var result = await _departmentTaskService.GetDepartmentTasksByDepartmentIdAsync(departmentId);

            if (!result.Success)
                return Handle(result);

            var dtoList = result.Value.Select(MapToDTO).ToList();

            return Handle(Result.Ok(dtoList));
        }

        //Tilføj --- Burde være post!
        [HttpPost("TilføjDepartmentTask")]
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
            var mappedModel = MapToUpdateDepartmentTaskModel(departmentTaskDTO, id);
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

        private DepartmentTaskModel MapToUpdateDepartmentTaskModel(UpdateDepartmentTaskDTO dto, int id)
        {
            return new DepartmentTaskModel
            {
                DepartmentTaskID = id,
                DepartmentTaskTopic = dto.DepartmentTaskTopic,
                EmployeeName = dto.EmployeeName,
                ShiftType = dto.ShiftType
            };
        }

        private DepartmentTaskDTO MapToDTO(DepartmentTaskModel model)
        {
            return new DepartmentTaskDTO
            {
                DepartmentTaskID = model.DepartmentTaskID,
                DepartmentID = model.DepartmentID,
                DepartmentTaskTopic = model.DepartmentTaskTopic,
                EmployeeName = model.EmployeeName,
                ShiftType = model.ShiftType
            };
        }
    }
}
