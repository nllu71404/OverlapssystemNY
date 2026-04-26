using System.Net.Http.Json;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;

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
        public async Task<List<MedicinModel>> GetMedicinByResidentId(int residentId)
        {
            return await _http.GetFromJsonAsync<List<MedicinModel>>($"api/Medicin/HentMedicinForBorger/{residentId}");
        }

        //Tilføj
        public async Task<int> AddMedicinTime(AddMedicinTimeDTO addMedicinTimeDTO)
        {
            var response = await _http.PostAsJsonAsync("api/Medicin/TilføjMedicin", addMedicinTimeDTO);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        //Update
        public async Task UpdateMedicin(int medicinTimeId, UpdateMedicinTimeDTO medicinDTO)
        {
            await _http.PutAsJsonAsync($"api/Medicin/{medicinTimeId}", medicinDTO);
        }

        //Delete
        public async Task DeleteMedicin(int medicinTimeId)
        {
            await _http.DeleteAsync($"api/Medicin/{medicinTimeId}");
        }


        public async Task SetMedicinChecked(int medicinTimeId, bool isChecked)
        {
            await _http.PutAsJsonAsync($"api/Medicin/SetChecked/{medicinTimeId}", new { isChecked });
        }

    }
}
