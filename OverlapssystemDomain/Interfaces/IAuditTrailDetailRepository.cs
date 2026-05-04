using OverlapssystemDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemDomain.Interfaces
{
    public interface IAuditTrailDetailRepository
    {
        Task<List<AuditTrailDetailModel>> GetAuditTrailDetailsByDepartmentIdAsync(int departmentId);
    }
}
