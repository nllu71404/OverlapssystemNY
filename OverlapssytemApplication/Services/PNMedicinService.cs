using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;

namespace OverlapssytemApplication.Services
{
    public class PNMedicinService : IPNMedicinService
    {

        private readonly IPNMedicinRepository _pnmedicinrepository;
        public PNMedicinService(IPNMedicinRepository pnmedicinrepository)
        {
            _pnmedicinrepository = pnmedicinrepository;
        }
        public async Task<List<PNMedicinModel>> GetPNMedicinByResidentIdAsync(int residentId)
        {
            return await _pnmedicinrepository.GetPNMedicinByResidentIdAsync(residentId);
        }
        public async Task DeletePNMedicinAsync(int pNMedicinId)
        {
            await _pnmedicinrepository.DeletePNMedicinAsync(pNMedicinId);
        }
        public async Task<int> AddPNMedicinAsync(int residentId, DateTime? pNTime, string reason)
        {
            var pNMedicin = new PNMedicinModel
            {
                ResidentID = residentId,
                PNTime = pNTime,
                Reason = reason
            };
            return await _pnmedicinrepository.SaveNewPNMedicinAsync(pNMedicin);
        }

        public async Task UpdatePNMedicinAsync(PNMedicinModel pNMedicin)
        {
            await _pnmedicinrepository.UpdatePNMedicinAsync(pNMedicin);
        }
    }
}
