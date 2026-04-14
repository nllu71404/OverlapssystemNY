using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssystemInfrastructure.Repositories;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Henter kun PN medicin for de sidste 48 timer
            var pnList = await _pnmedicinrepository.GetPNMedicinByResidentIdAsync(residentId);

            var cutoff = DateTime.Now.AddHours(-48);

            return pnList
                .Where(x => x.PNTime >= cutoff)
                .OrderByDescending(x => x.PNTime)
                .ToList();
        }
        public async Task DeletePNMedicinAsync(int pNMedicinId)
        {
            await _pnmedicinrepository.DeletePNMedicinAsync(pNMedicinId);
        }
        public async Task<int> AddPNMedicinAsync(int residentId, DateTime? pNTime, string reason)
        {
           
            //Hvis årsag ikke er udfyldt 
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Årsag er påkrævet");

            //Hvis tid er sat i fremtiden
            if (pNTime > DateTime.Now)
                throw new InvalidOperationException("Tid kan ikke registreres i fremtiden");

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
            //Hvis årsag ikke er udfyldt
            if (string.IsNullOrWhiteSpace(pNMedicin.Reason))
                throw new ArgumentException("Årsag er påkrævet");

            //Hvis tid er sat i fremtiden
            if (pNMedicin.PNTime > DateTime.Now)
                throw new Exception("Tid kan ikke registreres i fremtiden");

            await _pnmedicinrepository.UpdatePNMedicinAsync(pNMedicin);
        }
    }
}
