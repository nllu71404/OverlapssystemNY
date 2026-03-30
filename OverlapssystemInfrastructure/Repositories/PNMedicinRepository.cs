using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;

namespace OverlapssystemInfrastructure.Repositories
{
    internal class PNMedicinRepository : IMedicinRepository
    {
        public Task DeleteMedicinAsync(int medicinId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MedicinModel>> GetAllMedicinAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<MedicinModel>> GetMedicinByResidentIdAsync(int residentId)
        {
            throw new NotImplementedException();
        }

        public Task SaveNewMedicinAsync(MedicinModel medicin)
        {
            throw new NotImplementedException();
        }

        public Task ToggleMedicinGivenAsync(MedicinModel medTime)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMedicinAsync(MedicinModel medicin)
        {
            throw new NotImplementedException();
        }
    }
}
