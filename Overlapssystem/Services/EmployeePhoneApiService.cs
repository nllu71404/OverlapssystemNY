using System.Net.Http.Json;
using OverlapssystemDomain.Entities;

namespace Overlapssystem.Services
{
    public class EmployeePhoneApiService
    {
        private readonly HttpClient _http;
        public EmployeePhoneApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent Alle
        public async Task<List<EmployeePhoneModel>> GetAllEmployeePhoneNumbers()
        {
            return await _http.GetFromJsonAsync<List<EmployeePhoneModel>>("api/EmployeePhone/HentAlleEmployeePhones");
        }

        //Hent på Id
        public async Task<EmployeePhoneModel> GetEmployeePhoneById(int employeePhoneId)
        {
            return await _http.GetFromJsonAsync<EmployeePhoneModel>($"api/EmployeePhone/HentEmployeePhoneById/{employeePhoneId}");
        }

        public async Task<List<EmployeePhoneModel>> GetEmployeePhonesByDepartmentId(int departmentId)
        {
            return await _http.GetFromJsonAsync<List<EmployeePhoneModel>>($"api/EmployeePhone/HentEmployeePhonesByDepartmentId/{departmentId}");
        }

        //Tilføj
        public async Task SaveNewEmployeePhone(EmployeePhoneModel employeePhoneModel)
        {
            await _http.PostAsJsonAsync("api/EmployeePhone/TilføjEmployeePhone", employeePhoneModel);
        }

        //Update
        public async Task UpdateEmployeePhone(int employeePhoneId, EmployeePhoneModel employeePhoneModel)
        {
            await _http.PutAsJsonAsync($"api/EmployeePhone/OpdaterEmployeePhone/{employeePhoneId}", employeePhoneModel);
        }

        //Delete
        public async Task DeleteEmployeePhone(int employeePhoneId)
        {
            await _http.DeleteAsync($"api/EmployeePhone/SletEmployeePhone/{employeePhoneId}");
        }

    }
}
