using Overlapssystem.Services.Extensions;
using Overlapssystem.ViewModels;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using System.Net.Http.Json;

namespace Overlapssystem.Services
{
    public class DepartmentTaskApiService
    {
        private readonly HttpClient _http;
        public DepartmentTaskApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent
        public async Task<List<DepartmentTaskDTO>> GetAllDepartmentTask()
        {
            var response = await _http.GetAsync("api/DepartmentTask/HentDepartmentsTasks");
            var dtoList = await response.ReadApiResponse<List<DepartmentTaskDTO>>();
            return dtoList?.ToList() ?? new List<DepartmentTaskDTO>();
        }
        //Hent på ID
        public async Task<DepartmentTaskDTO> GetDepartmentTaskById(int id)
        {
            var response = await _http.GetAsync(
             $"api/DepartmentTask/HentDepartmentTasksID/{id}");

            return await response.ReadApiResponse<DepartmentTaskDTO>();
        }

        //Hent på department ID
        public async Task<List<DepartmentTaskDTO>> GetDepartmentTasksByDepartmentId(int departmentId)
        {
            var response = await _http.GetAsync(
            $"api/DepartmentTask/HentDepartmentTaskByDepartmentId/{departmentId}");

            return await response.ReadApiResponse<List<DepartmentTaskDTO>>();
        }

        //Tilføj
        public async Task<int> CreateDepartmentTask(AddDepartmentTaskDTO departmentTaskDto)
        {
            var response = await _http.PostAsJsonAsync(
             "api/DepartmentTask/TilføjDepartmentTask",
             departmentTaskDto);

            return await response.ReadApiResponse<int>();
        }

        //Update
        public async Task UpdateDepartmentTask(int id, UpdateDepartmentTaskDTO departmentTaskDto)
        {
            var response = await _http.PutAsJsonAsync(
           $"api/DepartmentTask/{id}",
           departmentTaskDto);

            await response.ReadApiResponse<object>();
        }

        //Delete
        public async Task DeleteDepartmentTask(int id)
        {
            var response = await _http.DeleteAsync(
             $"api/DepartmentTask/{id}");

            await response.ReadApiResponse<object>();
        }
    }
}
