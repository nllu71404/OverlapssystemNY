using Overlapssystem.Services.Extensions;
using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
using System.Net.Http.Json;

namespace Overlapssystem.Services
{
    public class DepartmentTaskApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<DepartmentTaskApiService> _logger;

        public DepartmentTaskApiService(HttpClient http, ILogger<DepartmentTaskApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET ALL
        public async Task<Result<List<DepartmentTaskDTO>>> GetAllDepartmentTask()
        {
            try
            {
                var response = await _http.GetAsync("api/DepartmentTask/HentDepartmentsTasks");

                var result = await response.ReadApiResponse<List<DepartmentTaskDTO>>();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllDepartmentTask fejlede");
                return Error.Technical("Kunne ikke hente opgaver");
            }
        }

        // GET BY ID
        public async Task<Result<DepartmentTaskDTO>> GetDepartmentTaskById(int id)
        {
            if (id <= 0)
                return Error.Validation("Ugyldigt id");

            try
            {
                var response = await _http.GetAsync(
                    $"api/DepartmentTask/HentDepartmentTasksID/{id}"
                );

                return await response.ReadApiResponse<DepartmentTaskDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetDepartmentTaskById fejlede for Id {Id}", id);
                return Error.Technical("Kunne ikke hente opgave");
            }
        }

        // GET BY DEPARTMENT
        public async Task<Result<List<DepartmentTaskDTO>>> GetDepartmentTasksByDepartmentId(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt departmentId");

            try
            {
                var response = await _http.GetAsync(
                    $"api/DepartmentTask/HentDepartmentTaskByDepartmentId/{departmentId}"
                );

                var result = await response.ReadApiResponse<List<DepartmentTaskDTO>>();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetDepartmentTasksByDepartmentId fejlede for DepartmentId {DepartmentId}", departmentId);
                return Error.Technical("Kunne ikke hente opgaver");
            }
        }

        // CREATE
        public async Task<Result<int>> CreateDepartmentTask(AddDepartmentTaskDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "api/DepartmentTask/TilføjDepartmentTask",
                    dto
                );

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateDepartmentTask fejlede");
                return Error.Technical("Kunne ikke oprette opgave");
            }
        }

        // UPDATE
        public async Task<Result> UpdateDepartmentTask(int id, UpdateDepartmentTaskDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/DepartmentTask/{id}",
                    dto
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateDepartmentTask fejlede for Id {Id}", id);
                return Error.Technical("Kunne ikke opdatere opgave");
            }
        }

        // DELETE
        public async Task<Result> DeleteDepartmentTask(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/DepartmentTask/{id}");

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteDepartmentTask fejlede for Id {Id}", id);
                return Error.Technical("Kunne ikke slette opgave");
            }
        }
    }
}
