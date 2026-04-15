using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssytemApplication.Interfaces
{
    public interface IDepartmentTaskService
    {   
            Task<List<DepartmentTaskModel>> GetAllDepartmentTasksAsync();
            Task<DepartmentTaskModel> GetDepartmentTaskByIdAsync(int departmentTaskId); 
            Task<List<DepartmentTaskModel>> GetDepartmentTasksByDepartmentIdAsync(int departmentId);
            Task SaveNewDepartmentTaskAsync(DepartmentTaskModel departmentTask);
            Task DeleteDepartmentTaskAsync(int departmentTaskId);
            Task UpdateDepartmentTaskAsync(DepartmentTaskModel departmentTask);

    }
}
