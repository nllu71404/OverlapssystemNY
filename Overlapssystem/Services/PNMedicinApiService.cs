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
        public async Task SaveNewPNMedicinTime(PNMedicinModel pnMedicin)
        {
            await _http.PostAsJsonAsync("api/PNMedicin/TilføjPNMedicin", pnMedicin);
        }

        //Update
        public async Task UpdatePNMedicinAsync(int pNMedicinId, PNMedicinModel pNMedicin)
        {
            await _http.PutAsJsonAsync($"api/PNMedicin/{pNMedicinId}", pNMedicin);
        }

        //Delete
        public async Task DeletePNMedicinAsync(int pNMedicinId)
        {
            await _http.DeleteAsync($"api/PNMedicin/{pNMedicinId}");
        }
    }
}
