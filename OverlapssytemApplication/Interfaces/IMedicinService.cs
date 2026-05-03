using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IMedicinService
    {
        Task<Result<List<MedicinModel>>> GetMedicinByResidentIdAsync(int residentId);
        Task<Result<int>> CreateMedicinTimeAsync(MedicinModel medicinModel);
        Task<Result> DeleteMedicinAsync(int medicinId);
        Task<Result> UpdateMedicinAsync(MedicinModel medicinModel);
        Task<Result> SetMedicinCheckedAsync(int medicinTimeId, bool isChecked);
    }
}
