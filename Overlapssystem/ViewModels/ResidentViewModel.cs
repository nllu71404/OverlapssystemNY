using OverlapssystemDomain.Enums;

namespace Overlapssystem.ViewModels
{
    public class ResidentViewModel
    {
      
            public int Id { get; set; }
            public int? DepartmentId { get; set; }
            public string Name { get; set; } = "";
            public string Status { get; set; } = "";
            public string Activity { get; set; } = "";
            public string Family { get; set; } = "";
            public string ResidentEmployee { get; set; } = "";

            public Risiko Risiko { get; set; }
            public Mood Mood { get; set; }

            public List<MedicinViewModel> MedicinTimes { get; set; } = new();
            public List<SpecialEventViewModel> SpecialEvents { get; set; } = new();
            public List<ShoppingViewModel> Shopping { get; set; } = new();
            public List<PNMedicinViewModel> PNMedicin { get; set; } = new();
    }
    }

