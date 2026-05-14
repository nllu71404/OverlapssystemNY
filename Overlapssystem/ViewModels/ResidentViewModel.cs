using OverlapssystemDomain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Overlapssystem.ViewModels
{
    public class ResidentViewModel
    {
      
            
            public int ResidentId { get; set; }
            public int? DepartmentId { get; set; }

            [Required(ErrorMessage = "Navn er påkrævet")]
            [StringLength(100, ErrorMessage = "Navn må maks være 100 tegn")]
            public string Name { get; set; } = "";

            [StringLength(800)]
            public string Status { get; set; } = "";

            [StringLength(800)]
            public string Activity { get; set; } = "";

            [StringLength(800)]
            public string Family { get; set; } = "";

            [StringLength(100)]
            public string ResidentEmployee { get; set; } = "";

            public Risiko Risiko { get; set; } 
            public Mood Mood { get; set; } 

            public List<MedicinViewModel> MedicinTimes { get; set; } = new();
            public List<SpecialEventViewModel> SpecialEvents { get; set; } = new();
            public List<ShoppingViewModel> Shopping { get; set; } = new();
            public List<PNMedicinViewModel> PNMedicin { get; set; } = new();
    }
    }

