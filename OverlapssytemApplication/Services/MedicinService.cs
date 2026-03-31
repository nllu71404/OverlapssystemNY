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
    internal class MedicinService : IMedicinService
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

            if (isChecked)
            {
                medicin.MedicinCheckTimeStamp = DateTime.UtcNow;
            }
            else
            {
                medicin.MedicinCheckTimeStamp = null;
            }

            await _medicinRepository.UpdateMedicinAsync(medicin);
        }
    }
}
