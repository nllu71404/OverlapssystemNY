using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class EmployeePhoneDTO
    {
        public int EmployeePhoneID { get; set; }
        public int? DepartmentID { get; set; }
        public string? EmployeeName { get; set; } = "";
        public string? PhoneNumber { get; set; } = "";
        public bool Test { get; set; }
    }
}
