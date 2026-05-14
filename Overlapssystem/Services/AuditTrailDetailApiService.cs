using Overlapssystem.Services.Extensions;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;

namespace Overlapssystem.Services
{
    public class AuditTrailDetailApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<AuditTrailDetailApiService> _logger;

        public AuditTrailDetailApiService(HttpClient http, ILogger<AuditTrailDetailApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<Result<List<AuditTrailDetailDTO>>> GetAuditTrailDetailsByDepartmentId(int departmentId)
        {
            if (departmentId <= 0)
                return Error.Validation("Ugyldigt departmentId");

            try
            {
                var response = await _http.GetAsync($"api/AuditTrailDetail/{departmentId}");

                var result = await response.ReadApiResponse<List<AuditTrailDetailDTO>>();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAuditTrailDetailsByDepartmentId fejlede for DepartmentId {DepartmentId}", departmentId);
                return Error.Technical("Kunne ikke hente audit trail detaljer");
            }
        }
    }
}