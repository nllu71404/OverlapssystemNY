using Overlapssystem.Services.Extensions;
using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
using System.Net.Http.Json;
using System.Reflection;

namespace Overlapssystem.Services
{
    public class EmployeePhoneApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<EmployeePhoneApiService> _logger;

        public EmployeePhoneApiService(HttpClient http, ILogger<EmployeePhoneApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET ALL
        public async Task<Result<List<EmployeePhoneDTO>>> GetAllEmployeePhoneNumbers()
        {
            try
            {
                var response = await _http.GetAsync(
                    "api/EmployeePhone/HentAlleEmployeePhones"
                );

                var result = await response.ReadApiResponse<List<EmployeePhoneDTO>>();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllEmployeePhoneNumbers fejlede");
                return Error.Technical("Kunne ikke hente telefonnumre");
            }
        }

        // GET BY ID
        public async Task<Result<EmployeePhoneDTO>> GetEmployeePhoneById(int employeePhoneId)
        {
            if (employeePhoneId <= 0)
                return Error.Validation("Ugyldigt id");

            try
            {
                var response = await _http.GetAsync(
                    $"api/EmployeePhone/HentEmployeePhoneById/{employeePhoneId}"
                );

                return await response.ReadApiResponse<EmployeePhoneDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEmployeePhoneById fejlede for Id {Id}", employeePhoneId);
                return Error.Technical("Kunne ikke hente telefonnummer");
            }
        }

        // GET BY DEPARTMENT
        public async Task<Result<List<EmployeePhoneDTO>>> GetEmployeePhonesByDepartmentId(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt departmentId");

            try
            {
                var response = await _http.GetAsync(
                    $"api/EmployeePhone/HentEmployeePhonesByDepartmentId/{departmentId}"
                );

                var result = await response.ReadApiResponse<List<EmployeePhoneDTO>>();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEmployeePhonesByDepartmentId fejlede for DepartmentId {DepartmentId}", departmentId);
                return Error.Technical("Kunne ikke hente telefonnumre");
            }
        }

        // ADD
        public async Task<Result<int>> AddEmployeePhone(AddEmployeePhoneDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "api/EmployeePhone/TilføjEmployeePhone",
                    dto
                );

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddEmployeePhone fejlede");
                return Error.Technical("Kunne ikke oprette telefonnummer");
            }
        }

        // UPDATE
        public async Task<Result> UpdateEmployeePhone(int id, EmployeePhoneDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/EmployeePhone/OpdaterEmployeePhone/{id}",
                    dto
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateEmployeePhone fejlede for Id {Id}", id);
                return Error.Technical("Kunne ikke opdatere telefonnummer");
            }
        }

        // DELETE
        public async Task<Result> DeleteEmployeePhone(int id)
        {
            try
            {
                var response = await _http.DeleteAsync(
                    $"api/EmployeePhone/SletEmployeePhone/{id}"
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteEmployeePhone fejlede for Id {Id}", id);
                return Error.Technical("Kunne ikke slette telefonnummer");
            }
        }
    }
}
