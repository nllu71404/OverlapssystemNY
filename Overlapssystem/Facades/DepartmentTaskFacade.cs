using Overlapssystem.Interfaces;
using Overlapssystem.ViewModels;
using OverlapssystemShared;
using Overlapssystem.Services;
using OverlapssytemApplication.Common.Result;

namespace Overlapssystem.Facades
{
    public class DepartmentTaskFacade : IDepartmentTaskFacade
    {

        private readonly DepartmentTaskApiService _departmentTaskApiService;

        public DepartmentTaskFacade(DepartmentTaskApiService departmentTaskApiService)
        {
            _departmentTaskApiService = departmentTaskApiService;
        }


        public async Task<Result<int>> AddDepartmentTask(DepartmentTaskViewModel vm)
        {
            var dto = MapAddDepartmentTask(vm);
            var departmentTaskId = await _departmentTaskApiService.CreateDepartmentTask(dto);
            return departmentTaskId;
        }

        public async Task<Result> DeleteDepartmentTask(int departmentTaskId)
        {
            var result = await _departmentTaskApiService.DeleteDepartmentTask(departmentTaskId);
           
            return result;
        }

        public async Task<Result> UpdateDepartmentTask(DepartmentTaskViewModel vm)
        {
            var dto = MapUpdateDepartmentTask(vm);
            var result = await _departmentTaskApiService.UpdateDepartmentTask(vm.DepartmentTaskID, dto);
            return result;
        }


        public async Task<Result<List<DepartmentTaskViewModel>>> GetDepartmentTasksByDepartment(int departmentId)
        {
            var departmentTasks = await _departmentTaskApiService.GetDepartmentTasksByDepartmentId(departmentId);
            
            
            return departmentTasks.Map(dtos =>
             {
                 var vms = dtos.Select(dto => MapDepartmentTask(dto)).ToList();
                 return vms;
             });

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