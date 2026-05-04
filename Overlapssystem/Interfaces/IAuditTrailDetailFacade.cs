using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;

namespace Overlapssystem.Interfaces
{
    public interface IAuditTrailDetailFacade
    {
        Task<List<AuditTrailDetailViewModel>> GetAuditTrailDetailsByDepartment(int departmentId);
    }
}
