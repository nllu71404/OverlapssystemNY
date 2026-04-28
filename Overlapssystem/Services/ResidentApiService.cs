using System.Net.Http.Json;
using System;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using Overlapssystem.Services.Extensions;


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
            var response = await _http.GetAsync("api/Resident/HenterResident");
            var residentList = await response.ReadApiResponse<List<ResidentDTO>>();
            return residentList?.ToList() ?? new List<ResidentDTO>();
        }

        public async Task<int> AddResident(AddResidentDTO resident)
        {
            var response = await _http.PostAsJsonAsync("api/Resident/OpretResident", resident);

            var residentId = await response.ReadApiResponse<int>();
            return residentId;
        }

        public async Task UpdateResident(int id, UpdateResidentDTO resident)
        {
            var response = await _http.PutAsJsonAsync($"api/Resident/{id}", resident);
            await response.ReadApiResponse<object>();
        }

        public async Task DeleteResident(int id)
        {
            var response = await _http.DeleteAsync($"api/Resident/{id}");
            await response.ReadApiResponse<object>();
        }

        public async Task<List<ResidentDTO>> GetByDepartment(int? id)
        {
            var response = await _http.GetAsync($"api/Resident/Department/{id}");
            var residentList = await response.ReadApiResponse<List<ResidentDTO>>();
            return residentList?.ToList() ?? new List<ResidentDTO>();
        }

       

    }
}
