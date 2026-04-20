using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;

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

        // Get all
        public async Task<Result<List<DepartmentModel>>> GetAllDepartmentsAsync()
        {
            try
            {
                var data = await _departmentRepository.GetAllDepartmentsAsync();

                Departments = data ?? new List<DepartmentModel>();

                return Departments; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente departments");
            }
        }

        // Get by id
        public async Task<Result<DepartmentModel>> GetDepartmentByIdAsync(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt department ID");

            try
            {
                var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);

                if (department == null)
                    return Error.NotFound("Department blev ikke fundet");

                return department; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Fejl ved hentning af department");
            }
        }

        // Get by name
        public async Task<Result<DepartmentModel>> GetDepartmentByNameAsync(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName))
                return Error.Validation("Department navn er påkrævet");

            try
            {
                var department = await _departmentRepository.GetDepartmentByNameAsync(departmentName);

                if (department == null)
                    return Error.NotFound("Department blev ikke fundet");

                return department; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Fejl ved hentning af department");
            }
        }

        // Create
        public async Task<Result<int>> SaveNewDepartmentAsync(DepartmentModel department)
        {
            if (department == null)
                return Error.Validation("Department må ikke være null");

            try
            {
                var id = await _departmentRepository.SaveNewDepartmentAsync(department);

                return id; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke oprette department");
            }
        }

        // Delete
        public async Task<Result> DeleteDepartmentAsync(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt department ID");

            try
            {
                await _departmentRepository.DeleteDepartmentAsync(departmentId);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Department findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke slette department");
            }
        }

        // Update
        public async Task<Result> UpdateDepartmentAsync(DepartmentModel department)
        {
            if (department == null)
                return Error.Validation("Department må ikke være null");

            try
            {
                await _departmentRepository.UpdateDepartmentAsync(department);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Department findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdatere department");
            }
        }
    }
}
