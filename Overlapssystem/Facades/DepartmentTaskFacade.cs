using Overlapssystem.Interfaces;
using Overlapssystem.ViewModels;
using OverlapssystemShared;
using Overlapssystem.Services;

namespace Overlapssystem.Facades
{
    public class DepartmentTaskFacade : IDepartmentTaskFacade
    {

        private readonly DepartmentTaskApiService _departmentTaskApiService;

        public DepartmentTaskFacade(DepartmentTaskApiService departmentTaskApiService)
        {
            _departmentTaskApiService = departmentTaskApiService;
        }


        public async Task<int> AddDepartmentTask(DepartmentTaskViewModel vm)
        {
            var dto = MapAddDepartmentTask(vm);
            var departmentTaskId = await _departmentTaskApiService.CreateDepartmentTask(dto);
            return departmentTaskId;
        }

        public async Task DeleteDepartmentTask(int departmentTaskId)
        {
            await _departmentTaskApiService.DeleteDepartmentTask(departmentTaskId);
        }

        public async Task UpdateDepartmentTask(DepartmentTaskViewModel vm)
        {
            var dto = MapUpdateDepartmentTask(vm);
            await _departmentTaskApiService.UpdateDepartmentTask(vm.DepartmentTaskID, dto);
        }


        public async Task<List<DepartmentTaskViewModel>> GetDepartmentTasksByDepartment(int departmentId)
        {
            var dtos = await _departmentTaskApiService.GetDepartmentTasksByDepartmentId(departmentId);
            var departmentTasks = dtos.Select(dto => MapDepartmentTask(dto)).ToList();
            return departmentTasks;
        }


        //------ MAPPING ------

        private UpdateDepartmentTaskDTO MapUpdateDepartmentTask(DepartmentTaskViewModel vm)
        {
            return new UpdateDepartmentTaskDTO
            {
                DepartmentTaskID = vm.DepartmentTaskID,
                DepartmentTaskTopic = vm.DepartmentTaskTopic,
                EmployeeName = vm.EmployeeName,
                ShiftType = vm.ShiftType

            };
        }

        private AddDepartmentTaskDTO MapAddDepartmentTask(DepartmentTaskViewModel vm)
        {
            return new AddDepartmentTaskDTO
            {
                DepartmentID = vm.DepartmentID,
                DepartmentTaskTopic = vm.DepartmentTaskTopic,
                EmployeeName = vm.EmployeeName,
                ShiftType = vm.ShiftType
            };
        }

        private DepartmentTaskViewModel MapDepartmentTask(DepartmentTaskDTO dto)
        {
            return new DepartmentTaskViewModel
            {
                DepartmentTaskID = dto.DepartmentTaskID,
                DepartmentID = dto.DepartmentID,
                DepartmentTaskTopic = dto.DepartmentTaskTopic,
                EmployeeName = dto.EmployeeName,
                ShiftType = dto.ShiftType
            };
        }
    }
}