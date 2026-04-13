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
    public class MedicinService : IMedicinService
    {
        private readonly IMedicinRepository _medicinRepository;

        public MedicinService(IMedicinRepository medicinRepository)
        {
            _medicinRepository = medicinRepository;
        }

        public async Task<List<MedicinModel>> GetMedicinByResidentIdAsync(int residentId)
        {
            return await _medicinRepository.GetMedicinByResidentIdAsync(residentId);
        }

        public async Task<int> AddMedicinTimeAsync(int residentId, DateTime? datetime)
        {
            var medTime = new MedicinModel
            {
                ResidentID = residentId,
                MedicinTime = datetime,
                MedicinCheckTimeStamp = null
            };
            return await _medicinRepository.SaveNewMedicinAsync(medTime);
        }

        public async Task DeleteMedicinAsync(int medicinId)
        {
            await _medicinRepository.DeleteMedicinAsync(medicinId);
        }

        public async Task UpdateMedicinAsync(MedicinModel medicinModel)
        {
            await _medicinRepository.UpdateMedicinAsync(medicinModel);
        }

        public async Task SetMedicinCheckedAsync(int medicinTimeId, bool isChecked)
        {
            await _medicinRepository.SetMedicinCheckedAsync(medicinTimeId, isChecked);
        }


    }
}
