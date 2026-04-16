using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
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

        //Hent alle
        public async Task<List<DepartmentTaskModel>> GetAllDepartmentTasksAsync()
        {
            return DepartmentTasks = await _departmentTaskRepository.GetAllDepartmentTasksAsync();
        }
        // Hent på TaskID
        public async Task<DepartmentTaskModel> GetDepartmentTaskByIdAsync(int departmentTaskId)
        {
            return await _departmentTaskRepository.GetDepartmentTaskByIdAsync(departmentTaskId);
        }

        //Hent på DepartmentID
        public async Task<List<DepartmentTaskModel>> GetDepartmentTasksByDepartmentIdAsync(int departmentId)
        {
            return DepartmentTasks = await _departmentTaskRepository.GetDepartmentTaskByDepartmentIdAsync(departmentId);
        }

        //Slet
        public async Task DeleteDepartmentTaskAsync(int departmentTaskId)
        {
            await _departmentTaskRepository.DeleteDepartmentTaskAsync(departmentTaskId);
        }
        //Tilføj ny
        public async Task SaveNewDepartmentTaskAsync(DepartmentTaskModel departmentTask)
        {
            await _departmentTaskRepository.SaveNewDepartmentTaskAsync(departmentTask);
        }
        //Update
        public async Task UpdateDepartmentTaskAsync(DepartmentTaskModel departmentTask)
        {
            await _departmentTaskRepository.UpdateDepartmentTaskAsync(departmentTask);
            
        }

        //Helper metode til at bestemme ShiftType baseret på det aktuelle tidspunkt
        public async Task<ShiftType> GetTimeOfDayAsync()
        {
            var now = DateTime.Now.TimeOfDay;

            if (now >= TimeSpan.FromHours(6) && now < TimeSpan.FromHours(18))
                return ShiftType.Dag;

            if (now >= TimeSpan.FromHours(18) && now < TimeSpan.FromHours(22))
                return ShiftType.Aften;

            return ShiftType.Nat;
        }
    }
}
