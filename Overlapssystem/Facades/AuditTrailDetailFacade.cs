using Overlapssystem.Services;
using Overlapssystem.ViewModels;
using OverlapssystemShared;
using Overlapssystem.Interfaces;
using OverlapssytemApplication.Common.Result;
using System.Linq;

namespace Overlapssystem.Facades
{
    public class AuditTrailDetailFacade : IAuditTrailDetailFacade
    {
        private readonly AuditTrailDetailApiService _auditTrailDetailApi; 

        public AuditTrailDetailFacade(AuditTrailDetailApiService auditTrailDetailApi)
        {
            _auditTrailDetailApi = auditTrailDetailApi;
        }

        public async Task<Result<List<AuditTrailDetailViewModel>>> GetAuditTrailDetailsByDepartment(int departmentId)
        {
            var result = await _auditTrailDetailApi.GetAuditTrailDetailsByDepartmentId(departmentId);

            return result.Map(dtos =>
                 dtos.Select(MapAuditTrailDetail).ToList()
            );
            
        }

        private AuditTrailDetailViewModel MapAuditTrailDetail(AuditTrailDetailDTO dto)
        {
            return new AuditTrailDetailViewModel
            {
                DepartmentID = dto.DepartmentID,
                AuditLogDetailId = dto.AuditLogDetailId,
                TableName = dto.TableName,
                PrimaryKeyValue = dto.PrimaryKeyValue,
                ColumnName = dto.ColumnName,
                OldValue = dto.OldValue,
                NewValue = dto.NewValue,
                Operation = dto.Operation,
                ChangedBy = dto.ChangedBy,
                ChangeDate = dto.ChangeDate
            };
        }
    }
}