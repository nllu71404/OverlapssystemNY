using OverlapssystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class ResidentDTO //Bruges til Get-funktioner
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

        public List<MedicinTimeDTO> MedicinTimes { get; set; } = new();
        public List<PNMedicinDTO> PNMedicin { get; set; } = new();
        public List<UpdateShoppingDTO> Shopping { get; set; } = new();
        public List<UpdateSpecialEventDTO> SpecialEvents { get; set; } = new();
    }
}
