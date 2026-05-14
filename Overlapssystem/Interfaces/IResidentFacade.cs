using Overlapssystem.ViewModels;
using OverlapssytemApplication.Common.Result;

namespace Overlapssystem.Interfaces
{
    public interface IResidentFacade
    {
        Task<Result> UpdateResident(ResidentViewModel vm);

        Task<Result<int>> AddResident(ResidentViewModel vm);

        Task<Result> DeleteResident(int residentId);
        Task<Result<List<ResidentViewModel>>> GetAllResidents();

        Task<Result<List<ResidentViewModel>>> GetResidentByDepartment(int departmentId);

        Task<Result> UpdateMedicin(MedicinViewModel vm);

        Task<Result<int>> AddMedicinTime(MedicinViewModel vm);

        Task<Result> DeleteMedicinTime(MedicinViewModel vm);    
        Task<Result> SetMedicinChecked(MedicinViewModel vm, bool isChecked);

        Task<Result> UpdatePNMedicin(PNMedicinViewModel vm);

        Task<Result<int>> AddPNMedicinTime(PNMedicinViewModel vm);
        Task<Result> DeletePNMedicinTime(PNMedicinViewModel vm);

        Task<Result> UpdateShopping(ShoppingViewModel vm);

        Task<Result<int>> AddShopping(ShoppingViewModel vm);
        Task<Result> DeleteShopping(ShoppingViewModel vm);

        Task<Result> UpdateSpecialEvent(SpecialEventViewModel vm);

        Task<Result<int>> AddSpecialEvent(SpecialEventViewModel vm);
        Task<Result> DeleteSpecialEvent(SpecialEventViewModel vm);
    }
}
