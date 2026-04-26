using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class UpdatePNMedicinDTO
    {
        public int PNMedicinID { get; set; }
        public int ResidentID { get; set; }
        public DateTime? PNTime { get; set; }
        public string Reason { get; set; } = "";
    }
}
