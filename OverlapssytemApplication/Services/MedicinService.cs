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

        // Marker medicin som givet eller ikke givet ved at opdatere MedicinCheckTimeStamp
        public async Task SetMedicinCheckedAsync(int medicinTimeId, bool isChecked) 
        {
            var medicin = await _medicinRepository.GetMedicinByIdAsync(medicinTimeId);

            medicin.IsChecked = isChecked;
            medicin.MedicinCheckTimeStamp = isChecked ? DateTime.UtcNow : null;

            await _medicinRepository.UpdateMedicinAsync(medicin);
        }
    }
}
