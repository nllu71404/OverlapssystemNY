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
        public string Activity { get; set; } = "";
        public string Family { get; set; } = "";
        public string ResidentEmployee { get; set; } = "";
        public Risiko Risiko { get; set; } = Risiko.Green;
        public Mood Mood { get; set; } = Mood.Neutral;

        public List<MedicinModel> MedicinTimes { get; set; } = new();

        public List<PNMedicinModel> PNMedicin { get; set; } = new();
        public List<ShoppingModel> Shopping { get; set; } = new();
        public List<SpecialEventModel> SpecialEvents { get; set; } = new();
        public ResidentModel() { }

        public ResidentModel(int residentId, int? departmentId, string name, string status, string activity, string family, string residentemployee , Risiko risiko, Mood mood)
        {
            ResidentId = residentId;
            DepartmentId = departmentId;
            Name = name;
            Status = status;
            Activity = activity;
            Family = family;
            ResidentEmployee = residentemployee;
            Risiko = risiko;
            Mood = mood;

        }
    }
}
