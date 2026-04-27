using OverlapssystemDomain.Entities;
using OverlapssystemShared;
namespace Overlapssystem.Services

{
    public class UserApiService
    {
        private readonly HttpClient _http;
        public UserApiService(HttpClient http)
        {
            _http = http;
        }

        //Hent alle
        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _http.GetFromJsonAsync<List<UserModel>>("api/Brugere/HenterBrugere");
        }
        //Hent på ID
        public async Task<UserModel> GetUserByID(int userID)
        {
            return await _http.GetFromJsonAsync<UserModel>($"api/Brugere/HenterBrugere/{userID}");
        }

        //Hent på brugernavn
        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await _http.GetFromJsonAsync<UserModel>($"api/Brugere/HenterBrugere/Brugernavn/{username}");
        }

        //Tilføj
        public async Task<int> CreateUser(AddUserDTO userDTO)
        {
            var response = await _http.PostAsJsonAsync($"api/Brugere/OpretBruger", userDTO);
            return await response.Content.ReadFromJsonAsync<int>();
        }
        //Delete
        public async Task DeleteUser(int userID)
        {
            await _http.DeleteAsync($"api/Brugere/{userID}");
        }
        //Update
        public async Task UpdateUser(int userID, AddUserDTO userDTO)
        {
            await _http.PutAsJsonAsync($"api/Brugere/{userID}", userDTO);
        }
        //Validering
        public async Task<bool> ValidateUser(string username, string password)
        {
            var dto = new { UserName = username, Password = password };
            var response = await _http.PostAsJsonAsync($"api/Brugere/ValiderBruger", dto);

            if (response.IsSuccessStatusCode)
            {
                var success = await response.Content.ReadFromJsonAsync<bool>();
                return success;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return false;

        }
    }
}
