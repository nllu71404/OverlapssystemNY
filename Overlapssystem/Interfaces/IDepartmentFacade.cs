using Overlapssystem.ViewModels;

namespace Overlapssystem.Interfaces
{
    public interface IDepartmentFacade
    {
        Task<int> AddDepartment(DepartmentViewModel vm);

        Task UpdateDepartment(DepartmentViewModel vm);

        Task DeleteDepartment(int departmentId);

        Task<List<DepartmentViewModel>> GetDepartments();

        Task<DepartmentViewModel> GetDepartmentById(int departmentId);

        Task<DepartmentViewModel> GetDepartmentByName(string departmentName);
    }
}
