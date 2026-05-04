using Overlapssystem.Services.Extensions;
using OverlapssystemShared;

namespace Overlapssystem.Services
{
    public class AuditTrailDetailApiService
    {
        private readonly HttpClient _http;

        public AuditTrailDetailApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AuditTrailDetailDTO>> GetAuditTrailDetailsByDepartmentId(int departmentId)
        {
            var response = await _http.GetAsync($"api/AuditTrailDetail/{departmentId}");

            var auditTrailDetails = await response.ReadApiResponse<List<AuditTrailDetailDTO>>();

            return auditTrailDetails ?? new List<AuditTrailDetailDTO>();
        }
    }
}