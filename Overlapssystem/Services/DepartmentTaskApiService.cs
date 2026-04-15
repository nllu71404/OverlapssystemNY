using OverlapssystemDomain.Entities;

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
        public async Task<List<DepartmentTaskModel>> GetAllDepartmentTask()
        {
            return await _http.GetFromJsonAsync<List<DepartmentTaskModel>>("api/DepartmentTask/HentDepartmentsTasks");
        }
        //Hent på ID
        public async Task<DepartmentTaskModel> GetDepartmentTaskById(int id)
        {
            return await _http.GetFromJsonAsync<DepartmentTaskModel>($"api/DepartmentTask/HentDepartmentTasksID/{id}");
        }

        //Tilføj
        public async Task CreateDepartmentTask(DepartmentTaskModel departmentTaskModel)
        {
            await _http.PostAsJsonAsync("api/DepartmentTask/TilføjDepartmentTask", departmentTaskModel);
        }

        //Update
        public async Task UpdateDepartmentTask(int id, DepartmentTaskModel departmentTaskModel)
        {
            await _http.PutAsJsonAsync($"api/DepartmentTask/{id}", departmentTaskModel);
        }

        //Delete
        public async Task DeleteDepartmentTask(int id)
        {
            await _http.DeleteAsync($"api/DepartmentTask/{id}");
        }
    }
}
