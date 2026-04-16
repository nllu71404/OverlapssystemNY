using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface ISpecialEventRepository
    {
        Task<List<SpecialEventModel>> GetAllSpecialEvents();
        Task<List<SpecialEventModel>> GetSpecialEventByResidentId(int residentId);
        Task<int> SaveNewSpecialEvent(SpecialEventModel specialEvent);
        Task UpdateSpecialEvent(SpecialEventModel specialEvent);
        Task DeleteSpecialEvent(int specialEventID);
    }
}
