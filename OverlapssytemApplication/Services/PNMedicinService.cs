using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
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
            if (residentId <= 0)
                return Error.Validation("Ugyldigt resident ID");

            try
            {
                var pnList = await _pnmedicinrepository.GetPNMedicinByResidentIdAsync(residentId);

                if (pnList == null)
                    return Error.NotFound("Ingen PN medicin fundet");

                var cutoff = DateTime.Now.AddHours(-48);

                var filtered = pnList
                    .Where(x => x.PNTime >= cutoff)
                    .OrderByDescending(x => x.PNTime)
                    .ToList();

                return filtered; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente PN medicin");
            }
        }

        public async Task<Result> DeletePNMedicinAsync(int pNMedicinId)
        {
            if (pNMedicinId <= 0)
                return Error.Validation("Ugyldigt ID");

            try
            {
                await _pnmedicinrepository.DeletePNMedicinAsync(pNMedicinId);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("PN medicin blev ikke fundet");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke slette PN medicin");
            }
        }

        public async Task<Result<int>> AddPNMedicinAsync(PNMedicinModel pNMedicinModel)
        {
           
            if (pNMedicinModel.ResidentID <= 0)
                return Error.Validation("Ugyldigt resident ID");

            try
            {
                var id = await _pnmedicinrepository.SaveNewPNMedicinAsync(pNMedicinModel);

                return id; // implicit success
            }
            catch (Exception ex)
            {
                return Error.Technical(ex.Message);
            }
        }

        public async Task<Result> UpdatePNMedicinAsync(PNMedicinModel pNMedicin)
        {
            if (pNMedicin == null)
                return Error.Validation("PN medicin må ikke være null");

            try
            {
                await _pnmedicinrepository.UpdatePNMedicinAsync(pNMedicin);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("PN medicin blev ikke fundet");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdatere PN medicin");
            }
        }
    }
}