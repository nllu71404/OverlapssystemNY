using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using Overlapssystem.Services.Extensions;

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
        public async Task<List<UpdateSpecialEventDTO>> GetSpecialEventByResidentID(int residentId)
        {
            var response = await _http.GetAsync($"api/SpecialEvent/HentSpecialEventForBorger/{residentId}");
            var dtoList = await response.ReadApiResponse<List<UpdateSpecialEventDTO>>();
            return dtoList?.ToList() ?? new List<UpdateSpecialEventDTO>();
        }

        //Tilføj
        public async Task<int> AddSpecialEvent(AddSpecialEventDTO addSpecialEventDTO)
        {
            var response = await _http.PostAsJsonAsync("api/SpecialEvent/TilføjSpecialEvent", addSpecialEventDTO);
            return await response.ReadApiResponse<int>();
        }

        //Update
        public async Task UpdateSpecialEvent(int specialEventID, UpdateSpecialEventDTO specialEventDto)
        {
            var response = await _http.PutAsJsonAsync($"api/SpecialEvent/{specialEventID}", specialEventDto);
            await response.ReadApiResponse<object>();
        }

        //Delete
        public async Task DeleteSpecialEvent(int specialEventID)
        {
            var response = await _http.DeleteAsync($"api/SpecialEvent/{specialEventID}");
            await response.ReadApiResponse<object>();
        }
    }
}
