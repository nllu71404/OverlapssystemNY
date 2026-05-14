using Overlapssystem.ViewModels;
using OverlapssytemApplication.Common.Result;

namespace Overlapssystem.Interfaces
{
    public interface IDepartmentTaskFacade
    {
        Task<Result<int>> AddDepartmentTask(DepartmentTaskViewModel vm);

        Task<Result> UpdateDepartmentTask(DepartmentTaskViewModel vm);

        Task<Result> DeleteDepartmentTask(int departmentTaskId);
        Task<Result<List<DepartmentTaskViewModel>>> GetDepartmentTasksByDepartment(int departmentId);

    }
}
