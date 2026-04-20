using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;

namespace OverlapssytemApplication.Interfaces
{
    public interface ISpecialEventService
    {
        Task<List<SpecialEventModel>> GetSpecialEventByResidentIdAsync(int residentId);
        Task<int> SaveNewSpecialEventAsync(SpecialEventModel specialEvent);
        Task UpdateSpecialEventAsync(SpecialEventModel specialEvent);
        Task DeleteSpecialEventAsync(int specialEventID);


    }
}
