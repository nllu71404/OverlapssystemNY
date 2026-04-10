using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Services;

namespace OverlapssytemApplication.Interfaces
{
    public interface IResidentServices
    {
        Task<List<ResidentModel>> LoadResidentsAsync();
        Task<List<ResidentModel>> LoadResidentsByDepartmentAsync(int departmentId);
        Task CreateResidentAsync(ResidentModel resident);
        Task UpdateResidentAsync(ResidentModel resident);
        Task DeleteResidentAsync(int residentId);
        
    }
}
