using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverlapssystemShared
{
    public class AddMedicinTimeDTO
    {
        public int ResidentID { get; set; }
        public TimeSpan? MedicinTime { get; set; }

        public bool IsChecked { get; set; }
    }
}
