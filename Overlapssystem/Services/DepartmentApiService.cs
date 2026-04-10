using System.Net.Http.Json;
using OverlapssystemDomain.Entities;

namespace Overlapssystem.Services
{
    public class DepartmentApiService
    {
        private readonly HttpClient _http;
        public DepartmentApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent Alle
        public async Task<List<DepartmentModel>> GetAllDepartments()
        {
            return await _http.GetFromJsonAsync<List<DepartmentModel>>("api/Department/HentAlleDepartments");
        }

        //Hent by Id
        public async Task<DepartmentModel> GetDepartmentById(int departmentId)
        {
            return await _http.GetFromJsonAsync<DepartmentModel>($"api/Department/HentAlleDepartmentsByID/{departmentId}");
        }

        //Hent by Name
        public async Task<DepartmentModel> GetDepartmentByName(string departmentName)
        {
            return await _http.GetFromJsonAsync<DepartmentModel>($"api/Department/HentAlleDepartmentsByName/{departmentName}");
        }

        //Tilføj
        public async Task SaveNewDepartment(DepartmentModel departmentModel)
        {
            await _http.PostAsJsonAsync("api/Department/TilføjAfdeling", departmentModel);
        }

        //Update
        public async Task UpdateDepartment(int departmentId, DepartmentModel departmentModel)
        {
            await _http.PutAsJsonAsync($"api/Department/{departmentId}", departmentModel);
        }

        //Delete
        public async Task DeleteDepartment(int departmentId)
        {
            await _http.DeleteAsync($"api/Department/{departmentId}");
        }
    }
}
