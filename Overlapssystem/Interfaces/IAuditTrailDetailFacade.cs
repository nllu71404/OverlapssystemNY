using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common.Result;

namespace Overlapssystem.Interfaces
{
    public interface IAuditTrailDetailFacade
    {
        Task<Result<List<AuditTrailDetailViewModel>>> GetAuditTrailDetailsByDepartment(int departmentId); 
    }
}
