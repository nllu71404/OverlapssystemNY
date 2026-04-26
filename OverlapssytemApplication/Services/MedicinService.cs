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

namespace OverlapssytemApplication.Services
{
    public class MedicinService : IMedicinService
    {
        private readonly IMedicinRepository _medicinRepository;

        public MedicinService(IMedicinRepository medicinRepository)
        {
            _medicinRepository = medicinRepository;
        }

        // Hent medicin
        public async Task<Result<List<MedicinModel>>> GetMedicinByResidentIdAsync(int residentId)
        {
            if (residentId <= 0)
                return Error.Validation("Ugyldigt resident ID");

            try
            {
                var result = await _medicinRepository.GetMedicinByResidentIdAsync(residentId);

                return result ?? new List<MedicinModel>(); // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente medicin");
            }
        }

        // Tilføj medicin
        public async Task<Result<int>> AddMedicinTimeAsync(MedicinModel medicinModel)
        {
            if (medicinModel == null)
                return Error.Validation("Medicin må ikke være null");

            try
            {
                var id = await _medicinRepository.SaveNewMedicinAsync(medicinModel);

                return id; // implicit success
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke oprette medicin");
            }
        }

        // Slet medicin
        public async Task<Result> DeleteMedicinAsync(int medicinId)
        {
            if (medicinId <= 0)
                return Error.Validation("Ugyldigt medicin ID");

            try
            {
                await _medicinRepository.DeleteMedicinAsync(medicinId);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Medicin blev ikke fundet");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke slette medicin");
            }
        }

        // Opdater medicin
        public async Task<Result> UpdateMedicinAsync(MedicinModel medicinModel)
        {
            if (medicinModel == null)
                return Error.Validation("Medicin må ikke være null");

            try
            {
                await _medicinRepository.UpdateMedicinAsync(medicinModel);

                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Medicin blev ikke fundet");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdatere medicin");
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

                if (medicin == null)
                    return Error.NotFound("Medicin blev ikke fundet");

                medicin.IsChecked = isChecked;
                medicin.MedicinCheckTimeStamp = isChecked ? DateTime.UtcNow : null;

                await _medicinRepository.UpdateMedicinAsync(medicin);

                return Result.Ok();
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdatere medicin status");
            }
        }
    }
}
