using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentModel>> GetAllDepartmentsAsync();
        Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId);
        Task<DepartmentModel> GetDepartmentByNameAsync(string departmentName);
        Task<int> SaveNewDepartmentAsync(DepartmentModel department);
        Task DeleteDepartmentAsync(int departmentId);
        Task UpdateDepartmentAsync(DepartmentModel department);
        Task<bool> ExistsAsync(int departmentId);


    }
}
