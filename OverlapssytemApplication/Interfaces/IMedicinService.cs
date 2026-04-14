using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssytemApplication.Interfaces
{
    public interface IMedicinService
    {
        Task<List<MedicinModel>> GetMedicinByResidentIdAsync(int residentId);
        Task<int> AddMedicinTimeAsync(MedicinModel medicinModel);
        Task DeleteMedicinAsync(int medicinId);
        Task UpdateMedicinAsync(MedicinModel medicinModel);
        Task SetMedicinCheckedAsync(int medicinTimeId, bool isChecked);
    }
}
