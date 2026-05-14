using Overlapssystem.ViewModels;
using OverlapssytemApplication.Common.Result;

namespace Overlapssystem.Interfaces
{
    public interface IEmployeePhoneFacade
    {
        Task<Result<int>> AddEmployeePhone(EmployeePhoneViewModel vm);

        Task<Result> UpdateEmployeePhone(EmployeePhoneViewModel vm);

        Task<Result> DeleteEmployeePhone(int employeePhoneId);
        Task<Result<List<EmployeePhoneViewModel>>> GetEmployeePhonesByDepartment(int departmentId);
    }
}
