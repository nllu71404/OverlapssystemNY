using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssystemInfrastructure.Repositories;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssytemApplication.Common;
using System.Reflection.Metadata.Ecma335;

namespace OverlapssytemApplication.Services
{
    public class PNMedicinService : IPNMedicinService
    {

        private readonly IPNMedicinRepository _pnmedicinrepository;

        public PNMedicinService(IPNMedicinRepository pnmedicinrepository)
        {
            _pnmedicinrepository = pnmedicinrepository;
        }

        public async Task<Result<List<PNMedicinModel>>> GetPNMedicinByResidentIdAsync(int residentId)
        {
            var pnList = await _pnmedicinrepository.GetPNMedicinByResidentIdAsync(residentId);

            if (pnList == null)
                return Result<List<PNMedicinModel>>.Fail("Ingen PN Medicin tider");

            var cutoff = DateTime.Now.AddHours(-48);

            var filtered = pnList
                .Where(x => x.PNTime >= cutoff)
                .OrderByDescending(x => x.PNTime)
                .ToList();

            return Result<List<PNMedicinModel>>.Ok(filtered);
        }

        public async Task<Result> DeletePNMedicinAsync(int pNMedicinId)
        {
            if (pNMedicinId <= 0)
                return Result.Fail("Ugyldigt ID");

            await _pnmedicinrepository.DeletePNMedicinAsync(pNMedicinId);

            return Result.Ok();
        }

        public async Task<Result<int>> AddPNMedicinAsync(
            int residentId, DateTime? pNTime, string reason)
        {
            //Ikke noget validering i serviceklassen da objektet oprettes med tomme værdier - skal valideres i UI pga automatisk opdatering

            var pNMedicin = new PNMedicinModel
            {
                ResidentID = residentId,
                PNTime = pNTime,
                Reason = reason
            };

            var id = await _pnmedicinrepository.SaveNewPNMedicinAsync(pNMedicin);

            return Result<int>.Ok(id);
        }

        public async Task<Result> UpdatePNMedicinAsync(PNMedicinModel pNMedicin)
        {
            //ikke noget validering i serviceklassen da objektet opdateres automatisk i UI - validering skal ske i UI

            await _pnmedicinrepository.UpdatePNMedicinAsync(pNMedicin);

            return Result.Ok();
        }
    }
}
