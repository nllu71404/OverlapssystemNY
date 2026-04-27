using Overlapssystem.ViewModels;

namespace Overlapssystem.Interfaces
{
    public interface IEmployeePhoneFacade
    {
        Task<int> AddEmployeePhone(EmployeePhoneViewModel vm);

        Task UpdateEmployeePhone(EmployeePhoneViewModel vm);

        Task DeleteEmployeePhone(int employeePhoneId);
        Task<List<EmployeePhoneViewModel>> GetEmployeePhonesByDepartment(int departmentId);
    }
}
