using Microsoft.AspNet.Identity;
using Overlapssystem.Services.Extensions;
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
            var response = await _http.GetAsync("api/User/HenterBrugere");
            var users = await response.ReadApiResponse<List<UserModel>>();
            return users?.ToList() ?? new List<UserModel>();
        }
        //Hent på ID
        public async Task<UserModel> GetUserByID(string userID)
        {
            var response = await _http.GetAsync($"api/User/HenterBrugere/{userID}");
            return await response.ReadApiResponse<UserModel>();
        }

        //Hent på brugernavn
        public async Task<UserModel> GetUserByUsername(string username)
        {
            var response = await _http.GetAsync($"api/User/HenterBrugere/Brugernavn/{username}");
            return await response.ReadApiResponse<UserModel>();
        }

        //Tilføj
        public async Task CreateUser(AddUserDTO userDTO)
        {
            var response = await _http.PostAsJsonAsync("api/User/OpretBruger", userDTO);
            await response.ReadApiResponse<object>();
        }
        //Delete
        public async Task DeleteUser(string userID)
        {
            var response = await _http.DeleteAsync($"api/User/{userID}");
            await response.ReadApiResponse<object>();
        }
        ////Update
        //public async Task UpdateUser(string userID, AddUserDTO userDTO)
        //{
        //    var response = await _http.PutAsJsonAsync($"api/User/{userID}", userDTO);
        //    await response.ReadApiResponse<object>();
        //}
        // Validering - returnerer JWT token ved succes, null ved fejl
        public async Task<string?> ValidateUser(string username, string password)
        {
            var dto = new { UserName = username, Password = password };
            var response = await _http.PostAsJsonAsync("api/User/ValiderBruger", dto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();
                var token = result?.Token;

                // Log the token for debugging purposes (remove in production)
                Console.WriteLine($"Generated Token: {token}");

                return token;
            }

            return null;
        }
    }
}
