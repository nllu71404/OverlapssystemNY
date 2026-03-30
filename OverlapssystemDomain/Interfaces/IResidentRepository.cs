using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;


namespace OverlapssystemDomain.Interfaces
{
    public interface IResidentRepository
    {
        Task<List<ResidentModel>> GetAllResidentsAsync();
        Task<List<ResidentModel>> GetResidentByDepartmentIdAsync(int departmentId);
        Task<int> SaveNewResidentAsync(ResidentModel resident);
        Task DeleteResidentAsync(int residentId);
        Task UpdateResidentAsync(ResidentModel resident);
    }
}
