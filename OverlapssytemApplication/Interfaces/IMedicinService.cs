using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Interfaces
{
    internal interface IMedicinService
    {
            Task SetMedicinCheckedAsync(int medicinTimeId, bool isChecked);
    }
}
