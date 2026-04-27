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
        public async Task<List<EmployeePhoneViewModel>> GetAllEmployeePhoneNumbers()
        {
            var response = await _http.GetAsync(
            "api/EmployeePhone/HentAlleEmployeePhones");

            var list = await response.ReadApiResponse<List<EmployeePhoneViewModel>>();

            return list ?? new List<EmployeePhoneViewModel>();
        }

        //Hent på Id
        public async Task<EmployeePhoneViewModel> GetEmployeePhoneById(int employeePhoneId)
        {
            var response = await _http.GetAsync(
            $"api/EmployeePhone/HentEmployeePhoneById/{employeePhoneId}");

            return await response.ReadApiResponse<EmployeePhoneViewModel>();
        }

        public async Task<List<EmployeePhoneViewModel>> GetEmployeePhonesByDepartmentId(int departmentId)
        {
            var response = await _http.GetAsync(
            $"api/EmployeePhone/HentEmployeePhonesByDepartmentId/{departmentId}");

            var list = await response.ReadApiResponse<List<EmployeePhoneViewModel>>();

            return list ?? new List<EmployeePhoneViewModel>();
        }

        //Tilføj
        public async Task AddNewEmployeePhone(EmployeePhoneViewModel employeePhoneModel)
        {
            var response = await _http.PostAsJsonAsync(
             "api/EmployeePhone/TilføjEmployeePhone",
             employeePhoneModel);

            await response.ReadApiResponse<object>();
        }

        //Update
        public async Task UpdateEmployeePhone(int employeePhoneId, EmployeePhoneViewModel employeePhoneModel)
        {
            var response = await _http.PutAsJsonAsync(
            $"api/EmployeePhone/OpdaterEmployeePhone/{employeePhoneId}",
            employeePhoneModel);

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
