using OverlapssystemDomain.Entities;
using OverlapssystemShared;

namespace Overlapssystem.Services
{
    public class SpecialEventApiService
    {
        private readonly HttpClient _http;
        public SpecialEventApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent
        public async Task<List<SpecialEventModel>> GetSpecialEventByResidentID(int residentId)
        {
            return await _http.GetFromJsonAsync<List<SpecialEventModel>>($"api/SpecialEvent/HentSpecialEventForBorger/{residentId}");
        }

        //Tilføj
        public async Task<int> AddSpecialEvent(AddSpecialEventDTO addSpecialEventDTO)
        {
            var response = await _http.PostAsJsonAsync("api/SpecialEvent/TilføjSpecialEvent", addSpecialEventDTO);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        //Update
        public async Task UpdateSpecialEvent(int specialEventID, UpdateSpecialEventDTO specialEventDto)
        {
            await _http.PutAsJsonAsync($"api/SpecialEvent/{specialEventID}", specialEventDto);
        }

        //Delete
        public async Task DeleteSpecialEvent(int specialEventID)
        {
            await _http.DeleteAsync($"api/SpecialEvent/{specialEventID}");
        }
    }
}
