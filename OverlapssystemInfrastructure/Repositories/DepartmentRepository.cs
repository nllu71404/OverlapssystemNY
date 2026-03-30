using OverlapssystemDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemInfrastructure.Repositories
{
    public class DepartmentRepository
    {
        public Task DeleteDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentModel>> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentModel> GetDepartmentByNameAsync(string departmentName)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveNewDepartmentAsync(DepartmentModel department)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDepartmentAsync(DepartmentModel department)
        {
            throw new NotImplementedException();
        }
    }
}
