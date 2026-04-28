using System.Net.Http.Json;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using Overlapssystem.Services.Extensions;

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
        public async Task<List<DepartmentDTO>> GetAllDepartments()
        {
            var response = await _http.GetAsync("api/Department/HentAlleDepartments");
            var departmentList = await response.ReadApiResponse<List<DepartmentDTO>>();
            return departmentList?.ToList() ?? new List<DepartmentDTO>();
        }

        //Hent by Id
        public async Task<DepartmentDTO> GetDepartmentById(int departmentId)
        {
            var response = await _http.GetAsync($"api/Department/HentAlleDepartmentsByID/{departmentId}");
            var department = await response.ReadApiResponse<DepartmentDTO>();
            return department;
        }

        //Hent by Name
        public async Task<DepartmentDTO> GetDepartmentByName(string departmentName)
        {
            var response = await _http.GetAsync($"api/Department/HentAlleDepartmentsByName/{departmentName}");
            var department = await response.ReadApiResponse<DepartmentDTO>();
            return department;
        }

        //Tilføj
        public async Task<int> AddDepartment(AddDepartmentDTO departmentDTO)
        {
            var response = await _http.PostAsJsonAsync("api/Department/TilføjAfdeling", departmentDTO);
            var departmentId = await response.ReadApiResponse<int>();
            return departmentId;
        }

        //Update
        public async Task UpdateDepartment(int departmentId, DepartmentDTO departmentDTO)
        {
            var response = await _http.PutAsJsonAsync($"api/Department/{departmentId}", departmentDTO);
            await response.ReadApiResponse<object>();
        }

        //Delete
        public async Task DeleteDepartment(int departmentId)
        {
            var response = await _http.DeleteAsync($"api/Department/{departmentId}");
            await response.ReadApiResponse<object>();
        }
    }
}
