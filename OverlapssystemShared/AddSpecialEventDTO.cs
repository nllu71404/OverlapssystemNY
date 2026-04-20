using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class AddSpecialEventDTO
    {
        public int? ResidentID { get; set; }
        public string? SpecialEventNote { get; set; }
        public DateTime? SpecialEventDateTime { get; set; }
    }
}
