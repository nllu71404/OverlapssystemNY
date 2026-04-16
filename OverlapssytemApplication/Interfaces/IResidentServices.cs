using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Services;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IResidentServices
    {
        Task<Result<List<ResidentModel>>> LoadResidentsAsync();

        Task<Result<List<ResidentModel>>> LoadResidentsByDepartmentAsync(int departmentId);

        Task<Result<int>> CreateResidentAsync(ResidentModel resident);

        Task<Result> UpdateResidentAsync(ResidentModel resident);

        Task<Result> DeleteResidentAsync(int residentId);

    }
}
