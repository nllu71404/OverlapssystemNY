using Microsoft.AspNetCore.Mvc;
using OverlapssytemApplication.Interfaces;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common;


namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditTrailDetailController : ApiControllerBase
    {

        private readonly IAuditTrailDetailService _auditTrailDetailService;

        public AuditTrailDetailController(IAuditTrailDetailService auditTrailDetailService)
        {
            _auditTrailDetailService = auditTrailDetailService;
        }
         
        // Hent audit trail details på DepartmentId
        [HttpGet("{departmentId:int}")]
        public async Task<IActionResult> GetAuditTrailDetailsByDepartmentId(int departmentId)
        {
            var result = await _auditTrailDetailService
                .LoadAuditTrailDetailsByDepartmentAsync(departmentId);

            if (!result.Success)
            {
                return Handle(result);
            }

            var auditTrailDetailDTOs = result.Value
                .Select(MapToGetAuditTrailDetailDTO)
                .ToList();

            return Handle(Result.Ok(auditTrailDetailDTOs)); ;
        }

        // Mapping metode
        private AuditTrailDetailDTO MapToGetAuditTrailDetailDTO(OverlapssystemDomain.Entities.AuditTrailDetailModel model)
        {
            return new AuditTrailDetailDTO
            {
                DepartmentID = model.DepartmentID,
                AuditLogDetailId = model.AuditLogDetailId,
                TableName = model.TableName,
                PrimaryKeyValue = model.PrimaryKeyValue,
                ColumnName = model.ColumnName,
                OldValue = model.OldValue,
                NewValue = model.NewValue,
                Operation = model.Operation,
                ChangedBy = model.ChangedBy,
                ChangeDate = model.ChangeDate
            };
        }
    }
}
