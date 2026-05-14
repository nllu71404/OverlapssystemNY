using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Interfaces
{
    public interface IAuditTrailDetailService
    {
        Task<Result<List<AuditTrailDetailModel>>> LoadAuditTrailDetailsByDepartmentAsync(int departmentId); 
    }
}
