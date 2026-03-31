using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IMedicinRepository
    {
        Task<List<MedicinModel>> GetAllMedicinAsync();
        Task<List<MedicinModel>> GetMedicinByResidentIdAsync(int residentId);
        Task<MedicinModel> GetMedicinByIdAsync(int medicinId);
        Task<int> SaveNewMedicinAsync(MedicinModel medicin);
        Task DeleteMedicinAsync(int medicinId);
        Task UpdateMedicinAsync(MedicinModel medicin);
        
    }
}
