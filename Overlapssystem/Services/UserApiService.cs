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
        public async Task<UserModel> GetUserByID(string userID)
        {
            return await _http.GetFromJsonAsync<UserModel>($"api/Brugere/HenterBrugere/{userID}");
        }

        //Hent på brugernavn
        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await _http.GetFromJsonAsync<UserModel>($"api/Brugere/HenterBrugere/Brugernavn/{username}");
        }

        //Tilføj
        public async Task CreateUser(AddUserDTO userDTO)
        {
            await _http.PostAsJsonAsync($"api/Brugere/OpretBruger", userDTO);
        }
        //Delete
        public async Task DeleteUser(string userID)
        {
            await _http.DeleteAsync($"api/Brugere/{userID}");
        }
        //Update
        public async Task UpdateUser(string userID, AddUserDTO userDTO)
        {
            await _http.PutAsJsonAsync($"api/Brugere/{userID}", userDTO);
        }
        // Validering - returnerer JWT token ved succes, null ved fejl
        public async Task<string?> ValidateUser(string username, string password)
        {
            //var dto = new { UserName = username, Password = password };
            //var response = await _http.PostAsJsonAsync("api/User/ValiderBruger", dto);

            //if (response.IsSuccessStatusCode)
            //{
            //    var result = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();
            //    return result?.Token;
            //}

            //return null;
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
