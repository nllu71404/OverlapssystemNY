using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;

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

        //Get all
        public async Task<Result<List<DepartmentModel>>> GetAllDepartmentsAsync()
        {
            var result = await _departmentRepository.GetAllDepartmentsAsync();

            Departments = result ?? new List<DepartmentModel>();

            return Result<List<DepartmentModel>>.Ok(Departments);
        }

        //Get by id
        public async Task<Result<DepartmentModel>> GetDepartmentByIdAsync(int departmentId)
        {
            var result = await _departmentRepository.GetDepartmentByIdAsync(departmentId);

            if (result == null)
                return Result<DepartmentModel>.Fail("Department blev ikke fundet");

            return Result<DepartmentModel>.Ok(result);
        }

        //Get by name
        public async Task<Result<DepartmentModel>> GetDepartmentByNameAsync(string departmentName)
        {
            var result = await _departmentRepository.GetDepartmentByNameAsync(departmentName);

            if (result == null)
                return Result<DepartmentModel>.Fail("Department blev ikke fundet");

            return Result<DepartmentModel>.Ok(result);
        }

        //Create
        public async Task<Result> SaveNewDepartmentAsync(DepartmentModel department)
        {
            try
            {
                await _departmentRepository.SaveNewDepartmentAsync(department);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        //Delete
        public async Task<Result> DeleteDepartmentAsync(int departmentId)
        {
            try
            {
                await _departmentRepository.DeleteDepartmentAsync(departmentId);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        //Update
        public async Task<Result> UpdateDepartmentAsync(DepartmentModel department)
        {
            try
            {
                await _departmentRepository.UpdateDepartmentAsync(department);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
