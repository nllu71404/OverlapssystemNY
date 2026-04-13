using OverlapssystemDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemDomain.Interfaces
{
     public interface IDepartmentTaskRepository
    {
        Task<List<DepartmentTaskModel>> GetAllDepartmentTasksAsync();
        Task<List<DepartmentTaskModel>> GetDepartmentTaskByDepartmentIdAsync(int departmentId);
        Task<DepartmentTaskModel> GetDepartmentTaskByIdAsync(int departmentTaskId);
        Task<int> SaveNewDepartmentTaskAsync(DepartmentTaskModel departmentTask);
        Task DeleteDepartmentTaskAsync(int departmentTaskId);
        Task UpdateDepartmentTaskAsync(DepartmentTaskModel departmentTask);

    }
}
