using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
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
            public async Task<Result<List<DepartmentTaskModel>>> GetAllDepartmentTasksAsync()
            {
                var result = await _departmentTaskRepository.GetAllDepartmentTasksAsync();

                DepartmentTasks = result ?? new List<DepartmentTaskModel>();

                return Result<List<DepartmentTaskModel>>.Ok(DepartmentTasks);
            }

            // Hent på TaskID
            public async Task<Result<DepartmentTaskModel>> GetDepartmentTaskByIdAsync(int departmentTaskId)
            {
                var result = await _departmentTaskRepository.GetDepartmentTaskByIdAsync(departmentTaskId);

                if (result == null)
                    return Result<DepartmentTaskModel>.Fail("Task blev ikke fundet");

                return Result<DepartmentTaskModel>.Ok(result);
            }

            //Hent på DepartmentID
            public async Task<Result<List<DepartmentTaskModel>>> GetDepartmentTasksByDepartmentIdAsync(int departmentId)
            {
                var result = await _departmentTaskRepository.GetDepartmentTaskByDepartmentIdAsync(departmentId);

                DepartmentTasks = result ?? new List<DepartmentTaskModel>();

                return Result<List<DepartmentTaskModel>>.Ok(DepartmentTasks);
            }

            //Slet
            public async Task<Result> DeleteDepartmentTaskAsync(int departmentTaskId)
            {
                try
                {
                    await _departmentTaskRepository.DeleteDepartmentTaskAsync(departmentTaskId);
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }

            //Tilføj ny
            public async Task<Result> SaveNewDepartmentTaskAsync(DepartmentTaskModel departmentTask)
            {
                try
                {
                    await _departmentTaskRepository.SaveNewDepartmentTaskAsync(departmentTask);
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }

            //Update
            public async Task<Result> UpdateDepartmentTaskAsync(DepartmentTaskModel departmentTask)
            {
                try
                {
                    await _departmentTaskRepository.UpdateDepartmentTaskAsync(departmentTask);
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }

            //Helper metode til at bestemme ShiftType baseret på det aktuelle tidspunkt
            public async Task<Result<ShiftType>> GetTimeOfDayAsync()
            {
                try
                {
                    var now = DateTime.Now.TimeOfDay;

                    ShiftType shift;

                    if (now >= TimeSpan.FromHours(6) && now < TimeSpan.FromHours(18))
                        shift = ShiftType.Dag;
                    else if (now >= TimeSpan.FromHours(18) && now < TimeSpan.FromHours(22))
                        shift = ShiftType.Aften;
                    else
                        shift = ShiftType.Nat;

                    return Result<ShiftType>.Ok(shift);
                }
                catch (Exception ex)
                {
                    return Result<ShiftType>.Fail(ex.Message);
                }
            }
        }
    }


    
