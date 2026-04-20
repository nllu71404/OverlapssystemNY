using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IDepartmentService
    {
        Task<Result<List<DepartmentModel>>> GetAllDepartmentsAsync();
        Task<Result<DepartmentModel>> GetDepartmentByIdAsync(int departmentId);
        Task<Result<DepartmentModel>> GetDepartmentByNameAsync(string departmentName);
        Task<Result> SaveNewDepartmentAsync(DepartmentModel department);
        Task<Result> DeleteDepartmentAsync(int departmentId);
        Task<Result> UpdateDepartmentAsync(DepartmentModel department);
    }
}
