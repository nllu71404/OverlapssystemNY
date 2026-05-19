using Overlapssystem.Services.Extensions;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Common.Result;
using System;
using System.Net.Http.Json;


namespace Overlapssystem.Services
{

    // Blazor frontend kan ikke kalde controlleren direkte, derfor bruges HttpClient i en API Service som sender Http request til backend
    public class ResidentApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ResidentApiService> _logger;

        public ResidentApiService(HttpClient http, ILogger<ResidentApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET ALL
        public async Task<Result<List<ResidentDTO>>> GetAllResidents()
        {
            try
            {
                var response = await _http.GetAsync("api/Resident/HenterResident");

                return await response.ReadApiResponse<List<ResidentDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllResidents fejlede");
                return Error.Technical("Kunne ikke hente beboere");
            }
        }

        // GET BY DEPARTMENT
        public async Task<Result<List<ResidentDTO>>> GetByDepartment(int? id)
        {

            try
            {
                var response = await _http.GetAsync($"api/Resident/Department/{id}");

                return await response.ReadApiResponse<List<ResidentDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByDepartment fejlede for DepartmentId {DepartmentId}", id);
                return Error.Technical("Kunne ikke hente beboere");
            }
        }

        // ADD
        public async Task<Result<int>> AddResident(AddResidentDTO resident)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Resident/OpretResident", resident);

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddResident fejlede");
                return Error.Technical("Kunne ikke oprette beboer");
            }
        }

        // UPDATE
        public async Task<Result> UpdateResident(int id, UpdateResidentDTO resident)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Resident/{id}", resident);

                var result = await response.ReadApiResponse<object>();

                    if (!result.Success)
                    return result.Error;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateResident fejlede for Id {ResidentId}", id);
                return Error.Technical("Kunne ikke opdatere beboer");
            }
        }

        // DELETE
        public async Task<Result> DeleteResident(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Resident/{id}");

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteResident fejlede for Id {ResidentId}", id);
                return Error.Technical("Kunne ikke slette beboer");
            }
        }
    }
}
