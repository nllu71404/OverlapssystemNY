using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class AuditTrailDetailDTO
    {
        public int? DepartmentID { get; set; }
        public int AuditLogDetailId { get; set; }
        public string? TableName { get; set; }
        public string? PrimaryKeyValue { get; set; }
        public string? ColumnName { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? Operation { get; set; }
        public string? ChangedBy { get; set; }
        public string? ChangeDate { get; set; }
    }
}
