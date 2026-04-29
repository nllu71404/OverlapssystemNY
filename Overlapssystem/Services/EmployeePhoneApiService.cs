using Overlapssystem.Services.Extensions;
using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using System.Net.Http.Json;
using System.Reflection;

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
        public async Task<List<EmployeePhoneDTO>> GetAllEmployeePhoneNumbers()
        {
            var response = await _http.GetAsync(
            "api/EmployeePhone/HentAlleEmployeePhones");

            var list = await response.ReadApiResponse<List<EmployeePhoneDTO>>();

            return list ?? new List<EmployeePhoneDTO>();
        }

        //Hent på Id
        public async Task<EmployeePhoneDTO> GetEmployeePhoneById(int employeePhoneId)
        {
            var response = await _http.GetAsync(
            $"api/EmployeePhone/HentEmployeePhoneById/{employeePhoneId}");

            return await response.ReadApiResponse<EmployeePhoneDTO>();
        }

        public async Task<List<EmployeePhoneDTO>> GetEmployeePhonesByDepartmentId(int departmentId)
        {
            var response = await _http.GetAsync(
            $"api/EmployeePhone/HentEmployeePhonesByDepartmentId/{departmentId}");

            var list = await response.ReadApiResponse<List<EmployeePhoneDTO>>();

            return list ?? new List<EmployeePhoneDTO>();
        }

        //Tilføj
        public async Task<int> AddEmployeePhone(AddEmployeePhoneDTO employeePhoneDTO)
        {
            var response = await _http.PostAsJsonAsync(
             "api/EmployeePhone/TilføjEmployeePhone",
             employeePhoneDTO);
            var result = await response.ReadApiResponse<int>();
            return result;
        }

        //Update
        public async Task UpdateEmployeePhone(int employeePhoneId, EmployeePhoneDTO employeePhoneDTO)
        {
            var response = await _http.PutAsJsonAsync(
            $"api/EmployeePhone/OpdaterEmployeePhone/{employeePhoneId}",
            employeePhoneDTO);
            await response.ReadApiResponse<object>();
        }

        //Delete
        public async Task DeleteEmployeePhone(int employeePhoneId)
        {
            var response = await _http.DeleteAsync(
            $"api/EmployeePhone/SletEmployeePhone/{employeePhoneId}");

            await response.ReadApiResponse<object>();
        }

    }
}
