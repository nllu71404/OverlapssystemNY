using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IPNMedicinRepository
    {
        Task<List<PNMedicinModel>> GetAllPNMedicinAsync();
        Task<List<PNMedicinModel>> GetPNMedicinByResidentIdAsync(int residentId);
        Task<int> SaveNewPNMedicinAsync(PNMedicinModel pNMedicin);
        Task DeletePNMedicinAsync(int pNMedicinId);
        Task UpdatePNMedicinAsync(PNMedicinModel pNMedicin);
    }
}
