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

        // GET ALL
        public async Task<Result<List<UserModel>>> GetAllUsers()
        {
            try
            {
                var response = await _http.GetAsync("api/User/HenterBrugere");
                return await response.ReadApiResponse<List<UserModel>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllUsers failed");
                return Error.Technical("Kunne ikke hente brugere");
            }
        }

        // GET BY ID
        public async Task<Result<UserModel>> GetUserByID(string userID)
        {
            try
            {
                var response = await _http.GetAsync($"api/User/HenterBrugere/{userID}");
                return await response.ReadApiResponse<UserModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserByID failed for {UserID}", userID);
                return Error.Technical("Kunne ikke hente bruger");
            }
        }

        // GET BY USERNAME
        public async Task<Result<UserModel>> GetUserByUsername(string username)
        {
            try
            {
                var response = await _http.GetAsync($"api/User/HenterBrugere/Brugernavn/{username}");
                return await response.ReadApiResponse<UserModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserByUsername failed for {Username}", username);
                return Error.Technical("Kunne ikke hente bruger");
            }
        }

        // CREATE
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

        // VALIDATE USER (JWT)
        public async Task<Result<string>> ValidateUser(string username, string password)
        {
            try
            {
                var dto = new { UserName = username, Password = password };

                var response = await _http.PostAsJsonAsync("api/User/ValiderBruger", dto);

                var result = await response.ReadApiResponse<TokenResponseDTO>();

                if (!result.Success)
                    return result.Error;

                return result.Map(x => x.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ValidateUser failed for {Username}", username);
                return Error.Technical("Login fejlede");
            }
        }
    }
}
