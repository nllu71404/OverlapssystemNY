using Overlapssystem.Services.Extensions;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssystemAPI.Common;
using System.Net.Http.Json;
using Overlapssystem.ViewModels;
using System.Net.Http.Json;


namespace Overlapssystem.Services
{
    public class MedicinApiService
    {
        private readonly HttpClient _http;
        public MedicinApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent
        public async Task<List<MedicinTimeDTO>> GetMedicinByResidentId(int residentId)
        {
            var response = await _http.GetAsync($"api/Medicin/HentMedicinForBorger/{residentId}");

            var dtoList = await response.ReadApiResponse<List<MedicinTimeDTO>>();

            return dtoList?.ToList() ?? new List<MedicinTimeDTO>();
        }

        //Tilføj
        public async Task<int> AddMedicinTime(AddMedicinTimeDTO addMedicinTimeDTO)
        {
            var response = await _http.PostAsJsonAsync("api/Medicin/TilføjMedicin", addMedicinTimeDTO);
            return await response.ReadApiResponse<int>();
        }

        //Update
        public async Task UpdateMedicin(int medicinTimeId, UpdateMedicinTimeDTO medicinDTO)
        {
            var response = await _http.PutAsJsonAsync(
                $"api/Medicin/{medicinTimeId}",
                medicinDTO);

            await response.ReadApiResponse<object>();
        }

        //Delete
        public async Task DeleteMedicin(int medicinTimeId)
        {
            var response = await _http.DeleteAsync(
                $"api/Medicin/{medicinTimeId}");

            await response.ReadApiResponse<object>();
        }


        public async Task SetMedicinChecked(int medicinTimeId, bool isChecked)
        {
            var response = await _http.PutAsJsonAsync($"api/Medicin/SetChecked/{medicinTimeId}", new { isChecked });
            await response.ReadApiResponse<object>();
        }

       

    }
}
