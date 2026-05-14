using Overlapssystem.Services.Extensions;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
using System.Net.Http.Json;

namespace Overlapssystem.Services
{
    public class DepartmentApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<DepartmentApiService> _logger;

        public DepartmentApiService(HttpClient http, ILogger<DepartmentApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET ALL
        public async Task<Result<List<DepartmentDTO>>> GetAllDepartments()
        {
            try
            {
                var response = await _http.GetAsync("api/Department/HentAlleDepartments");

                var result = await response.ReadApiResponse<List<DepartmentDTO>>();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllDepartments fejlede");
                return Error.Technical("Kunne ikke hente afdelinger");
            }
        }

        // GET BY ID
        public async Task<Result<DepartmentDTO>> GetDepartmentById(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt departmentId");

            try
            {
                var response = await _http.GetAsync(
                    $"api/Department/HentAlleDepartmentsByID/{departmentId}"
                );

                return await response.ReadApiResponse<DepartmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetDepartmentById fejlede for Id {DepartmentId}", departmentId);
                return Error.Technical("Kunne ikke hente afdeling");
            }
        }

        // GET BY NAME
        public async Task<Result<DepartmentDTO>> GetDepartmentByName(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName))
                return Error.Validation("Afdelingsnavn er ugyldigt");

            try
            {
                var response = await _http.GetAsync(
                    $"api/Department/HentAlleDepartmentsByName/{departmentName}"
                );

                return await response.ReadApiResponse<DepartmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetDepartmentByName fejlede for Name {DepartmentName}", departmentName);
                return Error.Technical("Kunne ikke hente afdeling");
            }
        }

        // ADD
        public async Task<Result<int>> AddDepartment(AddDepartmentDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "api/Department/TilføjAfdeling",
                    dto
                );

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddDepartment fejlede");
                return Error.Technical("Kunne ikke oprette afdeling");
            }
        }

        // UPDATE
        public async Task<Result> UpdateDepartment(int departmentId, DepartmentDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/Department/{departmentId}",
                    dto
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateDepartment fejlede for Id {DepartmentId}", departmentId);
                return Error.Technical("Kunne ikke opdatere afdeling");
            }
        }

        // DELETE
        public async Task<Result> DeleteDepartment(int departmentId)
        {
            try
            {
                var response = await _http.DeleteAsync(
                    $"api/Department/{departmentId}"
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteDepartment fejlede for Id {DepartmentId}", departmentId);
                return Error.Technical("Kunne ikke slette afdeling");
            }
        }
    }
}
