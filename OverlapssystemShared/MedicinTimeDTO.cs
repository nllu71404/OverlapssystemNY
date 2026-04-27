using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemShared
{
    public class MedicinTimeDTO
    {
        public int MedicinTimeID { get; set; }

        public int ResidentID { get; set; }

        public TimeSpan? MedicinTime { get; set; }

        public bool IsChecked { get; set; }

        public DateTime? MedicinCheckTimeStamp { get; set; }

    }
}
