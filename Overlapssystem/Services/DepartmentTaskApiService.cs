using System.Net.Http.Json;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using Overlapssystem.ViewModels;

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
            return await _http.GetFromJsonAsync<List<DepartmentTaskDTO>>("api/DepartmentTask/HentDepartmentsTasks");
        }
        //Hent på ID
        public async Task<DepartmentTaskDTO> GetDepartmentTaskById(int id)
        {
            return await _http.GetFromJsonAsync<DepartmentTaskDTO>($"api/DepartmentTask/HentDepartmentTasksID/{id}");
        }

        //Hent på department ID
        public async Task<List<DepartmentTaskDTO>> GetDepartmentTasksByDepartmentId(int departmentId)
        {
            return await _http.GetFromJsonAsync<List<DepartmentTaskDTO>>($"api/DepartmentTask/HentDepartmentTaskByDepartmentId/{departmentId}");
        }

        //Tilføj
        public async Task<int> CreateDepartmentTask(AddDepartmentTaskDTO departmentTaskDto)
        {
            var response = await _http.PutAsJsonAsync($"api/DepartmentTask/TilføjDepartmentTask", departmentTaskDto);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        //Update
        public async Task UpdateDepartmentTask(int id, UpdateDepartmentTaskDTO departmentTaskDto)
        {
            await _http.PutAsJsonAsync($"api/DepartmentTask/{id}", departmentTaskDto);
        }

        //Delete
        public async Task DeleteDepartmentTask(int id)
        {
            await _http.DeleteAsync($"api/DepartmentTask/{id}");
        }
    }
}
