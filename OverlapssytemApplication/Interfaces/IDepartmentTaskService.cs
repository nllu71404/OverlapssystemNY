using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IDepartmentTaskService
    {   
            Task<Result<List<DepartmentTaskModel>>> GetAllDepartmentTasksAsync();
            Task<Result<DepartmentTaskModel>> GetDepartmentTaskByIdAsync(int departmentTaskId); 
            Task<Result<List<DepartmentTaskModel>>> GetDepartmentTasksByDepartmentIdAsync(int departmentId);
            Task<Result<int>> SaveNewDepartmentTaskAsync(DepartmentTaskModel departmentTask);
            Task<Result> DeleteDepartmentTaskAsync(int departmentTaskId);
            Task<Result> UpdateDepartmentTaskAsync(DepartmentTaskModel departmentTask);
            Task<Result<ShiftType> > GetTimeOfDayAsync();

    }
}
