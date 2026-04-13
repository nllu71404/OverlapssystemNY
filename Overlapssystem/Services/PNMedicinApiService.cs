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
        public async Task<List<PNMedicinModel>> GetPNMedicinByResidentId(int residentId)
        {
            return await _http.GetFromJsonAsync<List<PNMedicinModel>>($"api/PNMedicin/PNMedicintider/{residentId}");
        }
        //Tilføj
        public async Task<int> AddPNMedicinTime(AddPNMedicinDTO addPNMedicinDTO)
        {
            var response = await _http.PostAsJsonAsync("api/PNMedicin/TilføjPNMedicin", addPNMedicinDTO);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        //Update
        public async Task UpdatePNMedicin(int pNMedicinId, PNMedicinModel pNMedicin)
        {
            await _http.PutAsJsonAsync($"api/PNMedicin/{pNMedicinId}", pNMedicin);
        }

        //Delete
        public async Task DeletePNMedicin(int pNMedicinId)
        {
            await _http.DeleteAsync($"api/PNMedicin/{pNMedicinId}");
        }
    }
}
