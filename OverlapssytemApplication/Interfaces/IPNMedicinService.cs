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
        Task SaveNewPNMedicinAsync(PNMedicinModel pNMedicin);
        Task UpdatePNMedicinAsync(PNMedicinModel pNMedicin);
        Task DeletePNMedicinAsync(int pNMedicinId);
    }
}
