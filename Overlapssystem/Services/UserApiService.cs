using Microsoft.AspNet.Identity;
using Overlapssystem.Services.Extensions;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
namespace Overlapssystem.Services

{
    public class UserApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<UserApiService> _logger;

        public UserApiService(HttpClient http, ILogger<UserApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // Hent alle
        public async Task<Result<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                var response = await _http.GetAsync("api/User/HenterBrugere");
                var users = await response.ReadApiResponse<List<UserDTO>>();
                var result = users ?? new List<UserDTO>();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllUsers failed");
                return Error.Technical("Kunne ikke hente brugere");
            }
        }

        // Hent på ID
        public async Task<Result<UserDTO>> GetUserByID(string userID)
        {
            try
            {
                var response = await _http.GetAsync($"api/User/HenterBrugere/{userID}");
                return await response.ReadApiResponse<UserDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserByID failed for {UserID}", userID);
                return Error.Technical("Kunne ikke hente bruger");
            }
        }

        // Hent på brugerenavn
        public async Task<Result<UserDTO>> GetUserByUsername(string username)
        {
            try
            {
                var response = await _http.GetAsync($"api/User/HenterBrugere/Brugernavn/{username}");
                return await response.ReadApiResponse<UserDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserByUsername failed for {Username}", username);
                return Error.Technical("Kunne ikke hente bruger");
            }
        }

        // Tilføj
        public async Task<Result> CreateUser(AddUserDTO userDTO)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/User/OpretBruger", userDTO);


                var result = await response.ReadApiResponse<object>();

                if (!result.Success)
                    return result.Error;

                return Result.Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateUser failed");
                return Error.Technical("Kunne ikke oprette bruger");
            }
        }

        // DELETE
        public async Task<Result> DeleteUser(string userID)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/User/{userID}");
                var result = await response.ReadApiResponse<object>();

                if (!result.Success)
                    return result.Error;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteUser failed for {UserID}", userID);
                return Error.Technical("Kunne ikke slette bruger");
            }
        }

        //Update
        public async Task<Result> UpdateUser(string userID, UpdateUserDTO userDTO)
        {
            var response = await _http.PutAsJsonAsync($"api/User/{userID}", userDTO);
            var result = await response.ReadApiResponse<object>();

            if (!result.Success)
                return result.Error;

            return Result.Ok();
        }


        // VALIDATE USER (JWT)
        public async Task<Result<string?>> ValidateUser(string username, string password)
        {
            try
            {
                var dto = new { UserName = username, Password = password };

                var response = await _http.PostAsJsonAsync("api/User/ValiderBruger", dto);

                var result = await response.ReadApiResponse<TokenResponseDTO>();

                if (!result.Success)
                    return result.Error;

                var token = result.Value?.Token;

                // Debug logging (remove in production)
                Console.WriteLine($"Generated Token: {token}");

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ValidateUser failed for {Username}", username);
                return Error.Technical("Login fejlede");
            }
        }
    }
}
