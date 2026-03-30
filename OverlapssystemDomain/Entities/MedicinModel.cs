using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemDomain.Entities
{
    public class MedicinModel
    {
        public int? MedicinTimeID { get; set; }
        public int? ResidentID { get; set; }
        public DateTime MedicinTime { get; set; }
        public DateTime? MedicinCheckTimeStamp { get; set; }

        public bool IsChecked => MedicinCheckTimeStamp.HasValue;
    }
}
