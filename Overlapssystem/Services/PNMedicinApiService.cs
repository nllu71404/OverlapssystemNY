using Overlapssystem.Services.Extensions;
using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
using static System.Net.WebRequestMethods;

namespace Overlapssystem.Services
{
    public class PNMedicinApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<PNMedicinApiService> _logger;

        public PNMedicinApiService(HttpClient http, ILogger<PNMedicinApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET
        public async Task<Result<List<PNMedicinDTO>>> GetPNMedicinByResidentId(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt resident id");

            try
            {
                var response = await _http.GetAsync(
                    $"api/PNMedicin/PNMedicintider/{residentId}"
                );

                return await response.ReadApiResponse<List<PNMedicinDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPNMedicinByResidentId fejlede for ResidentId {ResidentId}", residentId);
                return Error.Technical("Kunne ikke hente PN medicin");
            }
        }

        // ADD
        public async Task<Result<int>> AddPNMedicinTime(AddPNMedicinDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "api/PNMedicin/TilføjPNMedicin",
                    dto
                );

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddPNMedicinTime fejlede");
                return Error.Technical("Kunne ikke oprette PN medicin");
            }
        }

        // UPDATE
        public async Task<Result> UpdatePNMedicin(int id, UpdatePNMedicinDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/PNMedicin/{id}",
                    dto
                );

                var result = await response.ReadApiResponse<object>();

                if (!result.Success)
                    return result.Error;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdatePNMedicin fejlede for Id {PNMedicinId}", id);
                return Error.Technical("Kunne ikke opdatere PN medicin");
            }
        }

        // DELETE
        public async Task<Result> DeletePNMedicin(int id)
        {
            try
            {
                var response = await _http.DeleteAsync(
                    $"api/PNMedicin/{id}"
                );

                var result = await response.ReadApiResponse<object>();

                if (!result.Success)
                    return result.Error;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeletePNMedicin fejlede for Id {PNMedicinId}", id);
                return Error.Technical("Kunne ikke slette PN medicin");
            }
        }
    }
}
