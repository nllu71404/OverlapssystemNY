using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
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

        //Hent medicin
        public async Task<Result<List<MedicinModel>>> GetMedicinByResidentIdAsync(int residentId)
        {
            var result = await _medicinRepository.GetMedicinByResidentIdAsync(residentId);

            return Result<List<MedicinModel>>.Ok(result ?? new List<MedicinModel>());
        }

        //Tilføj medicin
        public async Task<Result<int>> AddMedicinTimeAsync(MedicinModel medicinModel)
        {
            try
            {
                var id = await _medicinRepository.SaveNewMedicinAsync(medicinModel);
                return Result<int>.Ok(id);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail(ex.Message);
            }
        }

        //Slet medicin
        public async Task<Result> DeleteMedicinAsync(int medicinId)
        {
            try
            {
                await _medicinRepository.DeleteMedicinAsync(medicinId);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        //Opdater medicin
        public async Task<Result> UpdateMedicinAsync(MedicinModel medicinModel)
        {
            try
            {
                await _medicinRepository.UpdateMedicinAsync(medicinModel);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        //Marker medicin som taget/ikke taget
        public async Task<Result> SetMedicinCheckedAsync(int medicinTimeId, bool isChecked)
        {
            try
            {
                var medicin = await _medicinRepository.GetMedicinByIdAsync(medicinTimeId);

                if (medicin == null)
                    return Result.Fail("Medicin blev ikke fundet");

                medicin.IsChecked = isChecked;
                medicin.MedicinCheckTimeStamp = isChecked ? DateTime.UtcNow : null;

                await _medicinRepository.UpdateMedicinAsync(medicin);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
