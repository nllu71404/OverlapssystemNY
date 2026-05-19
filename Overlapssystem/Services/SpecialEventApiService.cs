using Overlapssystem.Services.Extensions;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;

namespace Overlapssystem.Services
{
    public class SpecialEventApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<SpecialEventApiService> _logger;

        public SpecialEventApiService(HttpClient http, ILogger<SpecialEventApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // GET
        public async Task<Result<List<UpdateSpecialEventDTO>>> GetSpecialEventByResidentID(int residentId)
        {

            try
            {
                var response = await _http.GetAsync(
                    $"api/SpecialEvent/HentSpecialEventForBorger/{residentId}"
                );

                return await response.ReadApiResponse<List<UpdateSpecialEventDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSpecialEventByResidentID fejlede for ResidentId {ResidentId}", residentId);
                return Error.Technical("Kunne ikke hente special events");
            }
        }

        // ADD
        public async Task<Result<int>> AddSpecialEvent(AddSpecialEventDTO dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "api/SpecialEvent/TilføjSpecialEvent",
                    dto
                );

                return await response.ReadApiResponse<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddSpecialEvent fejlede");
                return Error.Technical("Kunne ikke oprette special event");
            }
        }

        // UPDATE
        public async Task<Result> UpdateSpecialEvent(int specialEventID, UpdateSpecialEventDTO dto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/SpecialEvent/{specialEventID}",
                    dto
                );

                var result = await response.ReadApiResponse<object>();

                if (!result.Success)
                    return result.Error;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateSpecialEvent fejlede for Id {SpecialEventID}", specialEventID);
                return Error.Technical("Kunne ikke opdatere special event");
            }
        }

        // DELETE
        public async Task<Result> DeleteSpecialEvent(int specialEventID)
        {
            try
            {
                var response = await _http.DeleteAsync(
                    $"api/SpecialEvent/{specialEventID}"
                );

                var result = await response.ReadApiResponse<object>();

                if (!result.Success)
                    return result.Error;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteSpecialEvent fejlede for Id {SpecialEventID}", specialEventID);
                return Error.Technical("Kunne ikke slette special event");
            }
        }
    }
}
