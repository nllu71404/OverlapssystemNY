using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IEmployeePhoneRepository
    {
        Task<List<EmployeePhoneModel>> GetAllEmployeePhoneNumbersAsync();
        Task<EmployeePhoneModel> GetEmployeePhoneByIdAsync(int employeePhoneId);
        Task<List<EmployeePhoneModel>> GetEmployeePhonesByDepartmentIdAsync(int departmentId);
        Task<int> SaveNewEmployeePhoneAsync(EmployeePhoneModel employeePhone);
        Task DeleteEmployeePhoneAsync(int employeePhoneId);
        Task UpdateEmployeePhoneAsync(EmployeePhoneModel employeePhone);
    }
}
