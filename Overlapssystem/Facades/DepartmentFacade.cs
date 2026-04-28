using Overlapssystem.Interfaces;
using Overlapssystem.ViewModels;
using Overlapssystem.Services;
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

        public async Task<int> AddDepartment(DepartmentViewModel vm)
        {
            var dto = MapAddDepartment(vm);
            var departmentId = await _departmentApiService.AddDepartment(dto);
            return departmentId;
        }

        public async Task DeleteDepartment(int departmentId)
        {
            await _departmentApiService.DeleteDepartment(departmentId);
        }

        public async Task<List<DepartmentViewModel>> GetDepartments()
        {
            var departments = await _departmentApiService.GetAllDepartments();
            return departments.Select(MapGetDepartment).ToList();
        }

        public async Task UpdateDepartment(DepartmentViewModel vm)
        {
            var dto = MapUpdateDepartment(vm);
            await _departmentApiService.UpdateDepartment(vm.DepartmentID, dto);
        }
        

        public async Task<DepartmentViewModel> GetDepartmentById(int departmentId)
        {
            var dto = await _departmentApiService.GetDepartmentById(departmentId);
            var department = MapGetDepartment(dto);
            return department;
        }

        public async Task<DepartmentViewModel> GetDepartmentByName(string name)
        {
            var dto = await _departmentApiService.GetDepartmentByName(name);
            var department = MapGetDepartment(dto);
            return department;
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
