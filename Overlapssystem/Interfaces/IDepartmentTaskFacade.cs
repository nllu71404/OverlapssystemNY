using Overlapssystem.ViewModels;

namespace Overlapssystem.Interfaces
{
    public interface IDepartmentTaskFacade
    {
        Task<int> AddDepartmentTask(DepartmentTaskViewModel vm);

        Task UpdateDepartmentTask(DepartmentTaskViewModel vm);

        Task DeleteDepartmentTask(int departmentTaskId);

        Task<List<DepartmentTaskViewModel>> GetDepartmentTasksByDepartment(int departmentId);

    }
}
