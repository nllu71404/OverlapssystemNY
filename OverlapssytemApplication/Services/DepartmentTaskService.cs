using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OverlapssytemApplication.Services
{
    public class DepartmentTaskService : IDepartmentTaskService
    {
        private readonly IDepartmentTaskRepository _departmentTaskRepository;

        public DepartmentTaskService(IDepartmentTaskRepository departmentTaskRepository)
        {
            _departmentTaskRepository = departmentTaskRepository;
        }

        public List<DepartmentTaskModel> DepartmentTasks { get; private set; } = new();

        // Hent alle
        public async Task<Result<List<DepartmentTaskModel>>> GetAllDepartmentTasksAsync()
        {
            try
            {
                var data = await _departmentTaskRepository.GetAllDepartmentTasksAsync();

                DepartmentTasks = data ?? new List<DepartmentTaskModel>();

                return DepartmentTasks; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente department tasks");
            }
        }

        // Hent på TaskID
        public async Task<Result<DepartmentTaskModel>> GetDepartmentTaskByIdAsync(int departmentTaskId)
        {
            if (departmentTaskId <= 0)
                return Error.Validation("Ugyldigt task ID");

            try
            {
                var task = await _departmentTaskRepository.GetDepartmentTaskByIdAsync(departmentTaskId);

                if (task == null)
                    return Error.NotFound("Task blev ikke fundet");

                return task; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Fejl ved hentning af task");
            }
        }

        // Hent på DepartmentID
        public async Task<Result<List<DepartmentTaskModel>>> GetDepartmentTasksByDepartmentIdAsync(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt department ID");

            try
            {
                var data = await _departmentTaskRepository.GetDepartmentTaskByDepartmentIdAsync(departmentId);

                DepartmentTasks = data ?? new List<DepartmentTaskModel>();

                return DepartmentTasks;
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente tasks for afdeling");
            }
        }

        // Slet
        public async Task<Result> DeleteDepartmentTaskAsync(int departmentTaskId)
        {
            if (departmentTaskId <= 0)
                return Error.Validation("Ugyldigt task ID");

            try
            {
                await _departmentTaskRepository.DeleteDepartmentTaskAsync(departmentTaskId);
                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Task findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke slette task");
            }
        }

        // Tilføj ny
        public async Task<Result<int>> SaveNewDepartmentTaskAsync(DepartmentTaskModel departmentTask)
        {
            if (departmentTask == null)
                return Error.Validation("Task må ikke være null");

            try
            {
                var newTaskId = await _departmentTaskRepository.SaveNewDepartmentTaskAsync(departmentTask);
                return Result.Ok(newTaskId);
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke oprette task");
            }
        }

        // Update
        public async Task<Result> UpdateDepartmentTaskAsync(DepartmentTaskModel departmentTask)
        {
            if (departmentTask == null)
                return Error.Validation("Task må ikke være null");

            try
            {
                await _departmentTaskRepository.UpdateDepartmentTaskAsync(departmentTask);
                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Task findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdatere task");
            }
        }

        // Helper metode
        public Task<Result<ShiftType>> GetTimeOfDayAsync()
        {
            var now = DateTime.Now.TimeOfDay;

            var shift =
                (now >= TimeSpan.FromHours(6) && now < TimeSpan.FromHours(18)) ? ShiftType.Dag :
                (now >= TimeSpan.FromHours(18) && now < TimeSpan.FromHours(22)) ? ShiftType.Aften :
                ShiftType.Nat;

            return Task.FromResult<Result<ShiftType>>(shift); // implicit success
        }
    }
}




