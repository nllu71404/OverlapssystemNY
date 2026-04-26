using OverlapssystemDomain.Enums;

namespace Overlapssystem.ViewModels
{
    public class DepartmentTaskViewModel
    {
        public int DepartmentTaskID { get; set; }
        public int? DepartmentID { get; set; }
        public string? DepartmentTaskTopic { get; set; }
        public string? EmployeeName { get; set; }
        public ShiftType ShiftType { get; set; }

    }
}
