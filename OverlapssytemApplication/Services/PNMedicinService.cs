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
using Microsoft.Extensions.Logging;

namespace OverlapssytemApplication.Services
{
    public class PNMedicinService : IPNMedicinService
    {
        private readonly IPNMedicinRepository _pnmedicinrepository;
        private readonly ILogger<PNMedicinService> _logger;

        public PNMedicinService(IPNMedicinRepository pnmedicinrepository, ILogger<PNMedicinService> logger)
        {
            _pnmedicinrepository = pnmedicinrepository;
            _logger = logger;
        }

        public async Task<Result<List<PNMedicinModel>>> GetPNMedicinByResidentIdAsync(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                var pnList = await _pnmedicinrepository.GetPNMedicinByResidentIdAsync(residentId);

                //Henter PN medicin inden for de sidste 48 timer og sorterer dem efter tid i faldende rækkefølge
                var cutoff = DateTime.Now.AddHours(-48);

                var filtered = pnList
                    .Where(x => x.PNTime >= cutoff)
                    .OrderByDescending(x => x.PNTime)
                    .ToList();

                return filtered; // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af PN medicintider");
                return Error.Technical("Kunne ikke hente PN medicintider");
            }
        }

        public async Task<Result> DeletePNMedicinAsync(int pNMedicinId)
        {
            if (pNMedicinId <= 0)
                return Error.Validation("Ugyldigt PN medicintid ID");

            try
            {
                await _pnmedicinrepository.DeletePNMedicinAsync(pNMedicinId);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "PN Medicintid blev ikke fundet");
                return Error.NotFound("Kunne ikke finde PN Medicintid at slette");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af PN medicintid");
                return Error.Technical("Kunne ikke slette PN medicintid");
            }
        }

        public async Task<Result<int>> CreatePNMedicinAsync(PNMedicinModel pNMedicinModel)
        {
           
            if (pNMedicinModel.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                var id = await _pnmedicinrepository.SaveNewPNMedicinAsync(pNMedicinModel);

                return id; // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af PN medicintid");
                return Error.Technical("Kunne ikke oprette PN medicintid");
            }
        }

        public async Task<Result> UpdatePNMedicinAsync(PNMedicinModel pNMedicin)
        {
            if (pNMedicin.PNMedicinID <= 0)
                return Error.Validation("Ugyldigt PN medicintid ID");

            if (pNMedicin.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                await _pnmedicinrepository.UpdatePNMedicinAsync(pNMedicin);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "PN medicintid blev ikke fundet");
                return Error.NotFound("Kunne ikke finde PN medicintid at opdatere");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af PN medicintid");
                return Error.Technical("Kunne ikke opdatere PN medicin");
            }
        }
    }
}