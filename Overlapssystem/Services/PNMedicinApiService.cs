using Overlapssystem.Services.Extensions;
using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using static System.Net.WebRequestMethods;

namespace Overlapssystem.Services
{
    public class PNMedicinApiService
    {
        private readonly HttpClient _http;
        public PNMedicinApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent
        public async Task<List<PNMedicinViewModel>> GetPNMedicinByResidentId(int residentId)
        {
            var response = await _http.GetAsync(
                $"api/PNMedicin/PNMedicintider/{residentId}");

            var dtoList = await response.ReadApiResponse<List<PNMedicinDTO>>();

            return dtoList?
                .Select(MapToViewModel)
                .ToList()
                ?? new List<PNMedicinViewModel>();
        }
        //Tilføj
        public async Task<int> AddPNMedicinTime(AddPNMedicinDTO addPNMedicinDTO)
        {
            var response = await _http.PostAsJsonAsync(
                "api/PNMedicin/TilføjPNMedicin",
                addPNMedicinDTO);

            return await response.ReadApiResponse<int>();
        }

        //Update
        public async Task UpdatePNMedicin(int pNMedicinId, UpdatePNMedicinDTO pNMedicinDto)
        {
            var response = await _http.PutAsJsonAsync(
               $"api/PNMedicin/{pNMedicinId}",
               pNMedicinDto);

            await response.ReadApiResponse<object>();
        }

        //Delete
        public async Task DeletePNMedicin(int pNMedicinId)
        {
            var response = await _http.DeleteAsync(
               $"api/PNMedicin/{pNMedicinId}");

            await response.ReadApiResponse<object>();
        }

        // ------ Mapping ------

        private PNMedicinViewModel MapToViewModel(PNMedicinDTO dto)
        {
            return new PNMedicinViewModel
            {
                PNMedicinID = dto.PNMedicinID,
                ResidentID = dto.ResidentID,
                Reason = dto.Reason,
                PNTime = dto.PNTime
               
            };
        }
    }
}
