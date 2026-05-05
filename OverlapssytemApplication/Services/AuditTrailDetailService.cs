using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common.Errors;
using Microsoft.Extensions.Logging;

namespace OverlapssytemApplication.Services
{
    public class AuditTrailDetailService : IAuditTrailDetailService
    {
        private readonly IAuditTrailDetailRepository _auditTrailDetailRepository;
        private readonly ILogger<AuditTrailDetailService> _logger;

        public AuditTrailDetailService(
            IAuditTrailDetailRepository auditTrailDetailRepository,
            ILogger<AuditTrailDetailService> logger)
        {
            _auditTrailDetailRepository = auditTrailDetailRepository;
            _logger = logger;
        }

        // Hent på DepartmentId
        public async Task<Result<List<AuditTrailDetailModel>>> LoadAuditTrailDetailsByDepartmentAsync(int departmentId)
        {
            if (departmentId <= 0) 
            {
                return Error.Validation("Ugyldigt afdelingsID");
            }

            try
            {
                var data = await _auditTrailDetailRepository
                    .GetAuditTrailDetailsByDepartmentIdAsync(departmentId);

                var auditTrailDetails = data ?? new List<AuditTrailDetailModel>();

                return auditTrailDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Fejl ved hentning af audit trail detaljer for afdeling {DepartmentId}",
                    departmentId);

                return Error.Technical("Fejl ved hentning af audit trail detaljer for afdeling");
            }
        }
    }
}
