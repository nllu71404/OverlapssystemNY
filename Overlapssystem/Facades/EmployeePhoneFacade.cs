using Overlapssystem.Interfaces;
using Overlapssystem.ViewModels;
using Overlapssystem.Services;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;


namespace Overlapssystem.Facades
{
    public class EmployeePhoneFacade : IEmployeePhoneFacade
    {
        private readonly EmployeePhoneApiService _employeePhoneApiService;

        public EmployeePhoneFacade(EmployeePhoneApiService employeePhoneApiService)
        {
            _employeePhoneApiService = employeePhoneApiService;
        }

        public async Task<Result<int>> AddEmployeePhone(EmployeePhoneViewModel vm)
        {
            var dto = MapAddEmployeePhone(vm);
            var employeePhoneId = await _employeePhoneApiService.AddEmployeePhone(dto);
            return employeePhoneId;
        }

        public async Task<Result> DeleteEmployeePhone(int employeePhoneId)
        {
            var result = await _employeePhoneApiService.DeleteEmployeePhone(employeePhoneId);
            return result;
        }

        public async Task<Result<List<EmployeePhoneViewModel>>> GetEmployeePhonesByDepartment(int departmentId)
        {
            var employeePhones = await _employeePhoneApiService.GetEmployeePhonesByDepartmentId(departmentId);
           
            return employeePhones.Map(dtos => dtos.Select(MapGetEmployeePhone).ToList());

        }

        public async Task<Result> UpdateEmployeePhone(EmployeePhoneViewModel vm)
        {
            var dto = MapUpdateEmployeePhone(vm);
            var result = await _employeePhoneApiService.UpdateEmployeePhone(vm.EmployeePhoneID, dto);
            return result;
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
