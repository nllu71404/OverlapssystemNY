using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IPNMedicinRepository
    {
        List<PNMedicinModel> GetAllPNMedicin();
        List<PNMedicinModel> GetPNMedicinByResidentId(int residentId);
        int SaveNewPNMedicin(PNMedicinModel pNMedicin);
        void DeletePNMedicin(int pNMedicinId);
        void UpdatePNMedicin(PNMedicinModel pNMedicin);
    }
}
