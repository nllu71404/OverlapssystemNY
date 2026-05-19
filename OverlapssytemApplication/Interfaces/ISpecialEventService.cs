using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface ISpecialEventService
    {
        Task<Result<List<SpecialEventModel>>> GetSpecialEventByResidentIdAsync(int residentId);
        Task<Result<int>> CreateSpecialEventAsync(SpecialEventModel specialEvent);
        Task<Result> UpdateSpecialEventAsync(SpecialEventModel specialEvent);
        Task<Result> DeleteSpecialEventAsync(int specialEventID);


    }
}
