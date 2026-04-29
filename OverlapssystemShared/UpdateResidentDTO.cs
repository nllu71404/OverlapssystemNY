using OverlapssystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class UpdateResidentDTO
    {
        public int ResidentId { get; set; }
        public string Name { get; set; } = "";
        public int? DepartmentId { get; set; }
        public string Status { get; set; } = "";
        public string Activity { get; set; } = "";
        public string Family { get; set; } = "";
        public string ResidentEmployee { get; set; } = "";

        public Risiko Risiko { get; set; }
        public Mood Mood { get; set; }
    }
}
