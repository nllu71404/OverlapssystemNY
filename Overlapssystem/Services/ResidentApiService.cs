using System.Net.Http.Json;
using System;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;

namespace Overlapssystem.Services
{

    // Blazor frontend kan ikke kalde controlleren direkte, derfor bruges HttpClient i en API Service som sender Http request til backend
    public class ResidentApiService
    {
        private readonly HttpClient _http;

        public ResidentApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ResidentDTO>> GetAllResidents()
        {
            return await _http.GetFromJsonAsync<List<ResidentDTO>>("api/Resident/HenterResident");
        }

        public async Task<int> AddResident(AddResidentDTO resident)
        {
            var response = await _http.PostAsJsonAsync("api/Resident/OpretResident", resident);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task UpdateResident(int id, UpdateResidentDTO resident)
        {
            await _http.PutAsJsonAsync($"api/Resident/{id}", resident);
        }

        public async Task DeleteResident(int id)
        {
            await _http.DeleteAsync($"api/Resident/{id}");
        }

        public async Task<List<ResidentDTO>> GetByDepartment(int? id)
        {
            return await _http.GetFromJsonAsync<List<ResidentDTO>>($"api/Resident/Department/{id}");
        }

        

    }
}
