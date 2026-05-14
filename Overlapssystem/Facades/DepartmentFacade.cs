using Overlapssystem.Interfaces;
using Overlapssystem.ViewModels;
using Overlapssystem.Services;
using OverlapssytemApplication.Common.Result;
using OverlapssystemShared;

namespace Overlapssystem.Facades
{
    public class DepartmentFacade : IDepartmentFacade
    {
        private readonly DepartmentApiService _departmentApiService;

        public DepartmentFacade(DepartmentApiService departmentApiService)
        {
            _departmentApiService = departmentApiService;
        }

        public async Task<Result<int>> AddDepartment(DepartmentViewModel vm)
        {
            var dto = MapAddDepartment(vm);
            var departmentId = await _departmentApiService.AddDepartment(dto);
            return departmentId;
        }

        public async Task<Result> DeleteDepartment(int departmentId)
        {
            var result = await _departmentApiService.DeleteDepartment(departmentId);
            return result;
        }

        public async Task<Result<List<DepartmentViewModel>>> GetDepartments()
        {
            var departments = await _departmentApiService.GetAllDepartments();

            return departments.Map(dtos =>
            dtos.Select(MapGetDepartment).ToList()
            );

        }

        public async Task<Result> UpdateDepartment(DepartmentViewModel vm)
        {
            var dto = MapUpdateDepartment(vm);
            var result = await _departmentApiService.UpdateDepartment(vm.DepartmentID, dto);
            return result;
        }
        

        public async Task<Result<DepartmentViewModel>> GetDepartmentById(int departmentId)
        {
            var department = await _departmentApiService.GetDepartmentById(departmentId);

            return department.Map(MapGetDepartment);
        }

        public async Task<Result<DepartmentViewModel>> GetDepartmentByName(string name)
        {
            var department = await _departmentApiService.GetDepartmentByName(name);
            return department.Map(MapGetDepartment);
        }

        // ----- Mapping -------

        private DepartmentDTO MapUpdateDepartment(DepartmentViewModel vm)
        {
            return new DepartmentDTO
            {
                DepartmentID = vm.DepartmentID,
                Name = vm.Name
            };
        }

        private AddDepartmentDTO MapAddDepartment(DepartmentViewModel vm)
        {
            return new AddDepartmentDTO
            {
                Name = vm.Name
            };
        }

        private DepartmentViewModel MapGetDepartment(DepartmentDTO dto)
        {
            return new DepartmentViewModel
            {
                DepartmentID = dto.DepartmentID,
                Name = dto.Name
            };
        }

       
    }
}
