using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Enums;

namespace OverlapssystemDomain.Entities
{
    public class ResidentModel
    {
        public int ResidentId { get; set; }
        public int? DepartmentId { get; set; }
        public string Name { get; set; } = "";
        public string Status { get; set; } = "";
        public Risiko Risiko { get; set; } = Risiko.Green;

        public List<MedicinModel> MedicinTimes { get; set; } = new();

        public List<PNMedicinModel> PNMedicin { get; set; } = new();

        public ResidentModel() { }

        public ResidentModel(int residentId, int? departmentId, string name, string status, Risiko risiko)
        {
            ResidentId = residentId;
            DepartmentId = departmentId;
            Name = name;
            Status = status;
            Risiko = risiko;
        }
    }
}
