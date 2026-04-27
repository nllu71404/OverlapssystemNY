using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IEmployeePhoneService
    {
        Task<Result<List<EmployeePhoneModel>>> GetAllEmployeePhoneNumbersAsync();
        Task<Result<EmployeePhoneModel>> GetEmployeePhoneByIdAsync(int employeePhoneId);
        Task<Result<List<EmployeePhoneModel>>> GetEmployeePhonesByDepartmentIdAsync(int departmentId);
        Task<Result<int>> SaveNewEmployeePhoneAsync(EmployeePhoneModel employeePhone);
        Task<Result> DeleteEmployeePhoneAsync(int employeePhoneId);
        Task<Result> UpdateEmployeePhoneAsync(EmployeePhoneModel employeePhone);

    }
}
