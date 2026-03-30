using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemDomain.Entities
{
    public class PNMedicinModel
    {
        public int? PNMedicinID { get; set; }
        public int ResidentID { get; set; }
        public DateTime PNTIME { get; set; }
        public string Reason { get; set; } = "";
    }
}
