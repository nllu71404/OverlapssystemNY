using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IPNMedicinService
    {
        Task<Result<List<PNMedicinModel>>> GetPNMedicinByResidentIdAsync(int residentId);

        Task<Result<int>> AddPNMedicinAsync(int residentId, DateTime? pNTime, string reason);

        Task<Result> UpdatePNMedicinAsync(PNMedicinModel pNMedicin);

        Task<Result> DeletePNMedicinAsync(int pNMedicinId);
    }
}
