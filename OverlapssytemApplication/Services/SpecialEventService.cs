using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Services
{
    public class SpecialEventService : ISpecialEventService //Mangler validation og error handling
    {
        private readonly ISpecialEventRepository _specialEventRepository;
        public SpecialEventService(ISpecialEventRepository specialEventRepository)
        {
            _specialEventRepository = specialEventRepository;
        }

        public async Task<Result<List<SpecialEventModel>>> GetSpecialEventByResidentIdAsync(int residentId)
        {
            return await _specialEventRepository.GetSpecialEventByResidentId(residentId);
        }

        public async Task<Result<int>> SaveNewSpecialEventAsync(SpecialEventModel specialEvent)
        {
            return await _specialEventRepository.SaveNewSpecialEvent(specialEvent);
        }

        public async Task<Result> UpdateSpecialEventAsync(SpecialEventModel specialEvent)
        {
            
             await _specialEventRepository.UpdateSpecialEvent(specialEvent);
            return Result.Ok();


        }
        public async Task<Result> DeleteSpecialEventAsync(int specialEventID)
        {
            await _specialEventRepository.DeleteSpecialEvent(specialEventID);
            return Result.Ok(); 
        }
    }
}
