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

        public async Task<List<ResidentModel>> GetAllResidents()
        {
            return await _http.GetFromJsonAsync<List<ResidentModel>>("api/Resident/HenterResident");
        }

        public async Task CreateResident(ResidentModel resident)
        {
            await _http.PostAsJsonAsync("api/Resident/OpretResident", resident);
        }

        public async Task UpdateResident(int id, ResidentModel resident)
        {
            await _http.PutAsJsonAsync($"api/Resident/{id}", resident);
        }

        public async Task DeleteResident(int id)
        {
            await _http.DeleteAsync($"api/Resident/{id}");
        }

        public async Task<List<ResidentModel>> GetByDepartment(int id)
        {
            return await _http.GetFromJsonAsync<List<ResidentModel>>($"api/Resident/Department/{id}");
        }

        public async Task AddMedicinTime(int residentid, DateTime dateTime)
        {
            var DTO = new AddMedicinTimeDTO { ResidentId = residentid, DateTime =  dateTime };
            await _http.PostAsJsonAsync($"api/Resident/MedicinGiven", DTO);
        }

        public async Task SetMedicinChecked(int medicinTimeId, bool isChecked)
        {
            await _http.PutAsJsonAsync($"api/Medicin/AngivMedicinTid", new {medicinTimeId, isChecked});
        }

    }
}
