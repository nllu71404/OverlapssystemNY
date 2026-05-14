using Overlapssystem.ViewModels;
using OverlapssytemApplication.Common.Result;

namespace Overlapssystem.Interfaces
{
    public interface IDepartmentFacade
    {
        Task<Result<int>> AddDepartment(DepartmentViewModel vm);

        Task<Result> UpdateDepartment(DepartmentViewModel vm);

        Task<Result> DeleteDepartment(int departmentId);

        Task<Result<List<DepartmentViewModel>>> GetDepartments();
        Task<Result<DepartmentViewModel>> GetDepartmentById(int departmentId);

        Task<Result<DepartmentViewModel>> GetDepartmentByName(string departmentName);
    }
}
