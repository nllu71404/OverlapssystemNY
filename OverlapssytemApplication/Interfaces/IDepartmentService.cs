using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssytemApplication.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentModel>> GetAllDepartmentsAsync();
        Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId);
        Task<DepartmentModel> GetDepartmentByNameAsync(string departmentName);
        Task SaveNewDepartmentAsync(DepartmentModel department);
        Task DeleteDepartmentAsync(int departmentId);
        Task UpdateDepartmentAsync(DepartmentModel department);
    }
}
