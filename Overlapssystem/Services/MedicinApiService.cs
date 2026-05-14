using Overlapssystem.Services.Extensions;
using Overlapssystem.ViewModels;
using OverlapssystemAPI.Common;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
using System.Net.Http.Json;
using System.Net.Http.Json;


namespace Overlapssystem.Services
{
    public class MedicinApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<MedicinApiService> _logger;

        public MedicinApiService(HttpClient http, ILogger<MedicinApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET
        public async Task<Result<List<MedicinTimeDTO>>> GetMedicinByResidentId(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt resident id");

            try
            {
                var response = await _http.GetAsync(
                    $"api/Medicin/HentMedicinForBorger/{residentId}"
                );

                return await response.ReadApiResponse<List<MedicinTimeDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetMedicinByResidentId fejlede for ResidentId {ResidentId}", residentId);
                return Error.Technical("Kunne ikke hente medicin");
            }
        }

        // ADD
        public async Task<Result<int>> AddMedicinTime(AddMedicinTimeDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "api/Medicin/TilføjMedicin",
                    dto
                );

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddMedicinTime fejlede");
                return Error.Technical("Kunne ikke oprette medicin");
            }
        }

        // UPDATE
        public async Task<Result> UpdateMedicin(int id, UpdateMedicinTimeDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/Medicin/{id}",
                    dto
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateMedicin fejlede for Id {MedicinId}", id);
                return Error.Technical("Kunne ikke opdatere medicin");
            }
        }

        // DELETE
        public async Task<Result> DeleteMedicin(int id)
        {
            try
            {
                var response = await _http.DeleteAsync(
                    $"api/Medicin/{id}"
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteMedicin fejlede for Id {MedicinId}", id);
                return Error.Technical("Kunne ikke slette medicin");
            }
        }

        // SET CHECKED
        public async Task<Result> SetMedicinChecked(int id, bool isChecked)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/Medicin/SetChecked/{id}",
                    new { isChecked }
                );

                await response.ReadApiResponse<object>();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetMedicinChecked fejlede for Id {MedicinId}", id);
                return Error.Technical("Kunne ikke opdatere medicin status");
            }
        }
    }
}
