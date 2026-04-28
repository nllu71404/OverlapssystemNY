using Overlapssystem.Interfaces;
using Overlapssystem.ViewModels;
using Overlapssystem.Services;
using OverlapssystemShared;


namespace Overlapssystem.Facades
{
    public class EmployeePhoneFacade : IEmployeePhoneFacade
    {
        private readonly EmployeePhoneApiService _employeePhoneApiService;

        public EmployeePhoneFacade(EmployeePhoneApiService employeePhoneApiService)
        {
            _employeePhoneApiService = employeePhoneApiService;
        }

        public async Task<int> AddEmployeePhone(EmployeePhoneViewModel vm)
        {
            var dto = MapAddEmployeePhone(vm);
            var employeePhoneId = await _employeePhoneApiService.AddEmployeePhone(dto);
            return employeePhoneId;
        }

        public async Task DeleteEmployeePhone(int employeePhoneId)
        {
            await _employeePhoneApiService.DeleteEmployeePhone(employeePhoneId);
        }

        public async Task<List<EmployeePhoneViewModel>> GetEmployeePhonesByDepartment(int departmentId)
        {
            var dtos = await _employeePhoneApiService.GetEmployeePhonesByDepartmentId(departmentId);
            var employeePhones = dtos.Select(MapGetEmployeePhone).ToList();
            return employeePhones;
        }

        public async Task UpdateEmployeePhone(EmployeePhoneViewModel vm)
        {
            var dto = MapUpdateEmployeePhone(vm);
            await _employeePhoneApiService.UpdateEmployeePhone(vm.EmployeePhoneID, dto);
        }


        // ----- Mapping -------

        private EmployeePhoneDTO MapUpdateEmployeePhone(EmployeePhoneViewModel vm)
        {
            return new EmployeePhoneDTO
            {
                EmployeePhoneID = vm.EmployeePhoneID,
                EmployeeName = vm.EmployeeName,
                PhoneNumber = vm.PhoneNumber,
                DepartmentID = vm.DepartmentID,
                Test = vm.Test
            };
        }

        private AddEmployeePhoneDTO MapAddEmployeePhone(EmployeePhoneViewModel vm)
        {
            return new AddEmployeePhoneDTO
            {
                EmployeeName = vm.EmployeeName,
                PhoneNumber = vm.PhoneNumber,
                DepartmentID = vm.DepartmentID,
                Test = vm.Test
            };
        }

        private EmployeePhoneViewModel MapGetEmployeePhone(EmployeePhoneDTO dto)
        {
            return new EmployeePhoneViewModel
            {
                EmployeePhoneID = dto.EmployeePhoneID,
                EmployeeName = dto.EmployeeName,
                PhoneNumber = dto.PhoneNumber,
                DepartmentID = dto.DepartmentID,
                Test = dto.Test
            };
        }
    }
}
