using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;

namespace OverlapssytemApplication.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentService()
        {
           
        }

        public List<DepartmentModel> Departments { get; private set; } = new();

        public async Task<List<DepartmentModel>> GetAllDepartmentsAsync()
        {
            return Departments = await _departmentService.GetAllDepartmentsAsync();
        }

        public async Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId)
        {
            return await _departmentService.GetDepartmentByIdAsync(departmentId);
        }

        public async Task<DepartmentModel> GetDepartmentByNameAsync(string departmentName)
        {
            return await _departmentService.GetDepartmentByNameAsync(departmentName);
        }

        public async Task SaveNewDepartmentAsync(DepartmentModel department)
        {
            await _departmentService.SaveNewDepartmentAsync(department);
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            await _departmentService.DeleteDepartmentAsync(departmentId);
        }
        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
             await _departmentService.UpdateDepartmentAsync(department);
        }
    }
}
