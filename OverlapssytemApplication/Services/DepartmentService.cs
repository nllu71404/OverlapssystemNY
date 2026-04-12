using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemDomain.Interfaces;

namespace OverlapssytemApplication.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
           _departmentRepository = departmentRepository;
        }

        public List<DepartmentModel> Departments { get; private set; } = new();

        public async Task<List<DepartmentModel>> GetAllDepartmentsAsync()
        {
            return Departments = await _departmentRepository.GetAllDepartmentsAsync();
        }

        public async Task<DepartmentModel> GetDepartmentByIdAsync(int departmentId)
        {
            return await _departmentRepository.GetDepartmentByIdAsync(departmentId);
        }

        public async Task<DepartmentModel> GetDepartmentByNameAsync(string departmentName)
        {
            return await _departmentRepository.GetDepartmentByNameAsync(departmentName);
        }

        public async Task SaveNewDepartmentAsync(DepartmentModel department)
        {
            await _departmentRepository.SaveNewDepartmentAsync(department);
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            await _departmentRepository.DeleteDepartmentAsync(departmentId);
        }
        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
             await _departmentRepository.UpdateDepartmentAsync(department);
        }
    }
}
