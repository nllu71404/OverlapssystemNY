using Overlapssystem.ViewModels;

namespace Overlapssystem.Interfaces
{
    public interface IResidentFacade
    {
        Task UpdateResident(ResidentViewModel vm);

        Task<int> AddResident(ResidentViewModel vm);

        Task DeleteResident(int residentId);

        Task<List<ResidentViewModel>> GetAllResidents();

        Task<List<ResidentViewModel>> GetResidentByDepartment(int departmentId);

        Task UpdateMedicin(MedicinViewModel vm);

        Task<int> AddMedicinTime(MedicinViewModel vm);

        Task DeleteMedicinTime(MedicinViewModel vm);

        Task SetMedicinChecked(MedicinViewModel vm, bool isChecked);

        Task UpdatePNMedicin(PNMedicinViewModel vm);

        Task<int> AddPNMedicinTime(PNMedicinViewModel vm);

        Task DeletePNMedicinTime(PNMedicinViewModel vm);

        Task UpdateShopping(ShoppingViewModel vm);

        Task<int> AddShopping(ShoppingViewModel vm);

        Task DeleteShopping(ShoppingViewModel vm);

        Task UpdateSpecialEvent(SpecialEventViewModel vm);

        Task<int> AddSpecialEvent(SpecialEventViewModel vm);

        Task DeleteSpecialEvent(SpecialEventViewModel vm);
    }
}
