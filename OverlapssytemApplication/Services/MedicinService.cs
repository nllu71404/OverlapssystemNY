using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OverlapssytemApplication.Services
{
    public class MedicinService : IMedicinService
    {
        private readonly IMedicinRepository _medicinRepository;
        private readonly ILogger<MedicinService> _logger;

        public MedicinService(IMedicinRepository medicinRepository, ILogger<MedicinService> logger)
        {
            _medicinRepository = medicinRepository;
            _logger = logger;
        }

        // Hent medicin
        public async Task<Result<List<MedicinModel>>> GetMedicinByResidentIdAsync(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            try
            {
                var result = await _medicinRepository.GetMedicinByResidentIdAsync(residentId);

                return result ?? new List<MedicinModel>(); // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af medicintider");
                return Error.Technical("Kunne ikke hente medicintider");
            }
        }

        // Tilføj medicin
        public async Task<Result<int>> AddMedicinTimeAsync(MedicinModel medicinModel)
        {
            if (medicinModel.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");
            try
            {
                var id = await _medicinRepository.SaveNewMedicinAsync(medicinModel);

                return id; // implicit success
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af medicintid");
                return Error.Technical("Kunne ikke oprette medicintid");
            }
        }

        // Slet medicin
        public async Task<Result> DeleteMedicinAsync(int medicinId)
        {
            if (medicinId <= 0)
                return Error.Validation("Ugyldigt medicintid ID");

            try
            {
                await _medicinRepository.DeleteMedicinAsync(medicinId);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Medicintid blev ikke fundet");
                return Error.NotFound("Kunne ikke finde medicintid at slette");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af medicintid");
                return Error.Technical("Kunne ikke slette medicintid");
            }
        }

        // Opdater medicin
        public async Task<Result> UpdateMedicinAsync(MedicinModel medicinModel)
        {
            if (medicinModel.ResidentID <= 0)
                return Error.Validation("Ugyldigt beboer ID");

            if (medicinModel.MedicinTimeID <= 0)
                return Error.Validation("Ugyldigt medicintid ID");

            try
            {
                await _medicinRepository.UpdateMedicinAsync(medicinModel);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Medicintid blev ikke fundet");
                return Error.NotFound("Kunne ikke finde medicintid at opdatere");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af medicintid");
                return Error.Technical("Kunne ikke opdatere medicintid");
            }
        }

        // Marker medicin som taget/ikke taget
        public async Task<Result> SetMedicinCheckedAsync(int medicinTimeId, bool isChecked)
        {
            if (medicinTimeId <= 0)
                return Error.Validation("Ugyldigt medicin ID");

            try
            {
                var medicin = await _medicinRepository.GetMedicinByIdAsync(medicinTimeId);

                medicin.IsChecked = isChecked;
                medicin.MedicinCheckTimeStamp = isChecked ? DateTime.UtcNow : null;

                await _medicinRepository.UpdateMedicinAsync(medicin);

                return Result.Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Medicintid blev ikke fundet");
                return Error.Technical("Medicintid kunne ikke findes ved opdatering af medicinstatus");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af medicinstatus");
                return Error.Technical("Kunne ikke opdatere medicinstatus");
            }
        }
    }
}
