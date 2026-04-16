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
    public class SpecialEventService : ISpecialEventService
    {
        private readonly ISpecialEventRepository _specialEventRepository;
        public SpecialEventService(ISpecialEventRepository specialEventRepository)
        {
            _specialEventRepository = specialEventRepository;
        }

        public async Task<List<SpecialEventModel>> GetSpecialEventByResidentIdAsync(int residentId)
        {
            return await _specialEventRepository.GetSpecialEventByResidentId(residentId);
        }

        public async Task<int> SaveNewSpecialEventAsync(SpecialEventModel specialEvent)
        {
            return await _specialEventRepository.SaveNewSpecialEvent(specialEvent);
        }

        public async Task UpdateSpecialEventAsync(SpecialEventModel specialEvent)
        {
            await _specialEventRepository.UpdateSpecialEvent(specialEvent);
        }

        public async Task DeleteSpecialEventAsync(int specialEventID)
        {
            await _specialEventRepository.DeleteSpecialEvent(specialEventID);
        }
    }
}
