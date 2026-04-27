using Overlapssystem.Services;
using Overlapssystem.ViewModels;
using OverlapssystemShared;
using Overlapssystem.Interfaces;

namespace Overlapssystem.Facades
{
    public class ResidentFacade : IResidentFacade
    {
        private readonly ResidentApiService _residentApi;
        private readonly MedicinApiService _medicinApi;
        private readonly PNMedicinApiService _pnMedicinApi;
        private readonly ShoppingApiService _shoppingApi;
        private readonly SpecialEventApiService _specialEventApi;

        public ResidentFacade(ResidentApiService residentApi, MedicinApiService medicinApi, PNMedicinApiService pnMedicinApi, ShoppingApiService shoppingApi, SpecialEventApiService specialEventApi)
        {
            _residentApi = residentApi;
            _medicinApi = medicinApi;
            _pnMedicinApi = pnMedicinApi;
            _shoppingApi = shoppingApi;
            _specialEventApi = specialEventApi;
        }


        // Resident

        public async Task UpdateResident(ResidentViewModel vm)
        {
            var dto = MapUpdateResident(vm);
            await _residentApi.UpdateResident(vm.Id, dto);
        }

        public async Task DeleteResident(int residentId)
        {
            await _residentApi.DeleteResident(residentId);
        }

        public async Task<int> AddResident(ResidentViewModel vm)
        {
            var dto = MapAddResident(vm);
            var residentId = await _residentApi.AddResident(dto);
            return residentId;
        }

        public async Task<List<ResidentViewModel>> GetAllResidents()
        {
            var dtos = await _residentApi.GetAllResidents();
            var residents = dtos.Select(dto => MapResident(dto)).ToList();
            return residents;
        }

        public async Task<List<ResidentViewModel>> GetResidentByDepartment(int departmentId)
        {
            var dtos = await _residentApi.GetByDepartment(departmentId);
            var residents = dtos.Select(dto => MapResident(dto)).ToList();
            return residents;
        }

        // Medicin


        public async Task UpdateMedicin(MedicinViewModel vm)
        {
            var dto = MapUpdateMedicin(vm);
            await _medicinApi.UpdateMedicin(vm.MedicinTimeID, dto);
        }

        public async Task<int> AddMedicinTime(MedicinViewModel vm)
        {
            var dto = MapAddMedicin(vm);
            var medicinTimeId = await _medicinApi.AddMedicinTime(dto);
            return medicinTimeId;
        }

        public async Task DeleteMedicinTime(MedicinViewModel vm) // evt ændre til kun at tage id som parameter
        {
            await _medicinApi.DeleteMedicin(vm.MedicinTimeID);
        }

        public async Task SetMedicinChecked(MedicinViewModel vm, bool isChecked)
        {
            await _medicinApi.SetMedicinChecked(vm.MedicinTimeID, isChecked);
        }

        // PNMedicin

        public async Task UpdatePNMedicin(PNMedicinViewModel vm)
        {
            var dto = MapUpdatePNMedicin(vm);
            await _pnMedicinApi.UpdatePNMedicin(vm.PNMedicinID, dto);
        }

        public async Task<int> AddPNMedicinTime(PNMedicinViewModel vm)
        {
            var dto = MapAddPNMedicin(vm);
            var pnMedicinTimeId = await _pnMedicinApi.AddPNMedicinTime(dto);
            return pnMedicinTimeId;
        }

        public async Task DeletePNMedicinTime(PNMedicinViewModel vm) // evt ændre til kun at tage id som parameter
        {
            await _pnMedicinApi.DeletePNMedicin(vm.PNMedicinID);
        }

        // Shopping

        public async Task UpdateShopping(ShoppingViewModel vm)
        {
            var dto = MapUpdateShopping(vm);
            await _shoppingApi.UpdateShopping(vm.ShoppingID, dto);
        }

        public async Task<int> AddShopping(ShoppingViewModel vm)
        {
            var dto = MapAddShopping(vm);
            var shoppingId = await _shoppingApi.AddShopping(dto);
            return shoppingId;
        }
        public async Task DeleteShopping(ShoppingViewModel vm) // evt ændre til kun at tage id som parameter
        {
            await _shoppingApi.DeleteShopping(vm.ShoppingID);
        }

        // SpecialEvent

        public async Task UpdateSpecialEvent(SpecialEventViewModel vm)
        {
            var dto = MapUpdateSpecialEvent(vm);
            await _specialEventApi.UpdateSpecialEvent(vm.SpecialEventID, dto);
        }

        public async Task<int> AddSpecialEvent(SpecialEventViewModel vm)
        {
            var dto = MapAddSpecialEvent(vm);
            var specialEventId = await _specialEventApi.AddSpecialEvent(dto);
            return specialEventId;
        }
        public async Task DeleteSpecialEvent(SpecialEventViewModel vm) // evt ændre til kun at tage id som parameter
        {
            await _specialEventApi.DeleteSpecialEvent(vm.SpecialEventID);
        }


        // --------- MAPPING ------------


        // Resident
        private UpdateResidentDTO MapUpdateResident(ResidentViewModel vm)
        {
            return new UpdateResidentDTO
            {
                Id = vm.Id,
                DepartmentId = vm.DepartmentId,
                Name = vm.Name,
                Status = vm.Status,
                Activity = vm.Activity,
                Family = vm.Family,
                ResidentEmployee = vm.ResidentEmployee,
                Risiko = vm.Risiko,
                Mood = vm.Mood
            };
        }

        private AddResidentDTO MapAddResident(ResidentViewModel vm)
        {
            return new AddResidentDTO
            {
                Name = vm.Name,
                DepartmentId = vm.DepartmentId,
                Status = vm.Status,
                Activity = vm.Activity,
                Family = vm.Family,
                ResidentEmployee = vm.ResidentEmployee,
                Risiko = vm.Risiko,
                Mood = vm.Mood
            };
        }

        private ResidentViewModel MapResident(ResidentDTO dto)
        {
            return new ResidentViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                Status = dto.Status,
                Activity = dto.Activity,
                Family = dto.Family,
                ResidentEmployee = dto.ResidentEmployee,
                Risiko = dto.Risiko,
                Mood = dto.Mood,

                 MedicinTimes = dto.MedicinTimes?
            .Select(MapMedicin)
            .ToList() ?? new List<MedicinViewModel>(),

                PNMedicin = dto.PNMedicin?
            .Select(MapPNMedicin)
            .ToList() ?? new List<PNMedicinViewModel>(),

                Shopping = dto.Shopping?
            .Select(MapShopping)
            .ToList() ?? new List<ShoppingViewModel>(),

                SpecialEvents = dto.SpecialEvents?
            .Select(MapSpecialEvent)
            .ToList() ?? new List<SpecialEventViewModel>()
            };
        }

        // Medicin
        private UpdateMedicinTimeDTO MapUpdateMedicin(MedicinViewModel vm)
        {
            return new UpdateMedicinTimeDTO
            {
                MedicinTimeID = vm.MedicinTimeID,
                MedicinTime = TimeSpan.TryParse(vm.MedicinTimeText, out var ts) ? ts : null,
                IsChecked = vm.IsChecked
            };
        }

        private AddMedicinTimeDTO MapAddMedicin(MedicinViewModel vm)
        {
            return new AddMedicinTimeDTO
            {
                ResidentId = vm.ResidentID,
                MedicinTime = TimeSpan.TryParse(vm.MedicinTimeText, out var ts) ? ts : null,
                IsChecked = vm.IsChecked
            };
        }

        private MedicinViewModel MapMedicin(MedicinTimeDTO dto) //Child mapping - bruges i MapResident
        {
            return new MedicinViewModel
            {
                MedicinTimeID = dto.MedicinTimeID,
                ResidentID = dto.ResidentID,
                MedicinTimeText = dto.MedicinTime?.ToString(@"hh\:mm") ?? string.Empty,
                IsChecked = dto.IsChecked,
                MedicinCheckTimeStampText = dto.MedicinCheckTimeStamp?.ToString("HH:mm") ?? string.Empty
            };
        }

        // PNMedicin
        private UpdatePNMedicinDTO MapUpdatePNMedicin(PNMedicinViewModel vm)
        {
            return new UpdatePNMedicinDTO
            {
                PNMedicinID = vm.PNMedicinID,
                PNTime = vm.PNTime,
                Reason = vm.Reason
            };


        }

        private AddPNMedicinDTO MapAddPNMedicin(PNMedicinViewModel vm)
        {
            return new AddPNMedicinDTO
            {
                ResidentID = vm.ResidentID,
                PNTime = vm.PNTime,
                Reason = vm.Reason
            };
        }

        private PNMedicinViewModel MapPNMedicin(PNMedicinDTO dto) //Child mapping - bruges i MapResident
        {
            return new PNMedicinViewModel
            {
                PNMedicinID = dto.PNMedicinID,
                ResidentID = dto.ResidentID,
                PNTime = dto.PNTime,
                Reason = dto.Reason
            };
        }

        // Shopping
        private UpdateShoppingDTO MapUpdateShopping(ShoppingViewModel vm)
        {
            return new UpdateShoppingDTO
            {
                ShoppingID = vm.ShoppingID,
                ResidentID = vm.ResidentID,
                Day = vm.Day,
                Time = TimeSpan.TryParse(vm.TimeText, out var ts) ? ts : null,
                PaymentMethod = vm.PaymentMethod
            };
        }

        private AddShoppingDTO MapAddShopping(ShoppingViewModel vm)
        {
            return new AddShoppingDTO
            {
                ResidentID = vm.ResidentID,
                Day = vm.Day,
                Time = TimeSpan.TryParse(vm.TimeText, out var ts) ? ts : null,
                PaymentMethod = vm.PaymentMethod
            };
        }

        private ShoppingViewModel MapShopping(UpdateShoppingDTO dto) // Child mapping - bruges i MapResident
        {
            return new ShoppingViewModel
            {
                ShoppingID = dto.ShoppingID,
                ResidentID = dto.ResidentID,
                Day = dto.Day,
                TimeText = dto.Time?.ToString(@"hh\:mm") ?? string.Empty,
                PaymentMethod = dto.PaymentMethod
            };
        }

        // SpecialEvent

        private UpdateSpecialEventDTO MapUpdateSpecialEvent(SpecialEventViewModel vm)
        {
            return new UpdateSpecialEventDTO
            {
                SpecialEventID = vm.SpecialEventID,
                ResidentID = vm.ResidentID,
                SpecialEventNote = vm.SpecialEventNote,
                SpecialEventDateTime = DateTime.TryParse(vm.SpecialEventDateTimeText, out var dt) ? dt : (DateTime?)null
            };
        }

        private AddSpecialEventDTO MapAddSpecialEvent(SpecialEventViewModel vm)
        {
            return new AddSpecialEventDTO
            {
                ResidentID = vm.ResidentID,
                SpecialEventNote = vm.SpecialEventNote,
                SpecialEventDateTime = DateTime.TryParse(vm.SpecialEventDateTimeText, out var dt) ? dt : (DateTime?)null
            };
        }

        private SpecialEventViewModel MapSpecialEvent(UpdateSpecialEventDTO dto) // Child mapping - bruges i MapResident
        {
            return new SpecialEventViewModel
            {
                SpecialEventID = dto.SpecialEventID,
                ResidentID = dto.ResidentID,
                SpecialEventNote = dto.SpecialEventNote,
                SpecialEventDateTimeText = dto.SpecialEventDateTime?.ToString("yyyy-MM-dd HH:mm") ?? string.Empty
            };
        }
    }
}
