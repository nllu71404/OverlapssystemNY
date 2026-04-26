using OverlapssystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class UpdateDepartmentTaskDTO
    {
        public int DepartmentTaskID { get; set; }
        public string? DepartmentTaskTopic { get; set; }
        public string? EmployeeName { get; set; }

        public ShiftType ShiftType { get; set; }
    }
}
