using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssytemApplication.Interfaces
{
    public interface IPNMedicinService
    {
        Task<List<PNMedicinModel>> GetPNMedicinByResidentIdAsync(int residentId);
        Task<int> AddPNMedicinAsync(int residentId, DateTime? pNTime, string reason);
        Task UpdatePNMedicinAsync(PNMedicinModel pNMedicin);
        Task DeletePNMedicinAsync(int pNMedicinId);
    }
}
