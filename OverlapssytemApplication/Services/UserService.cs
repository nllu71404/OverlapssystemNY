using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Interfaces;

namespace OverlapssytemApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserModel> _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository, IPasswordHasher<UserModel> passwordHasher, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<List<UserModel>>> GetAllUsersAsync()
        {
            try
            {
                var data = await _userRepository.GetAllUsers();
                var users = data ?? new List<UserModel>();
                return Result.Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all users");
                return Error.Technical("Kunne ikke hente brugere");
            }
        }

        public async Task<Result<UserModel>> GetUserByIdAsync(int userID)
        {
            if (userID <= 0) return Error.Validation("Ugyldigt bruger-id");

            try
            {
                var user = await _userRepository.GetUserByID(userID);
                if (user == null) return Error.NotFound("Brugeren blev ikke fundet");
                return Result.Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved indlæsning af bruger med id {UserId}", userID);
                return Error.Technical("Fejl ved indlæsning af bruger");
            }
        }

        public async Task<Result<UserModel>> GetUserByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return Error.Validation("Brugernavn er påkrævet");

            try
            {
                var user = await _userRepository.GetUserByUserName(userName);
                if (user == null) return Error.NotFound("Brugeren blev ikke fundet");
                return Result.Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved indlæsning af brugernavn {UserName}", userName);
                return Error.Technical("Fejl ved indlæsning af brugernavn");
            }
        }

        public async Task<Result> DeleteUserAsync(int id)
        {
            if (id <= 0) return Error.Validation("Ugyldigt bruger-id");

            try
            {
                await _userRepository.DeleteUser(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Brugeren findes ikke");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kunne ikke slette bruger med id {UserId}", id);
                return Error.Technical("Kunne ikke slette brugeren");
            }
        }

        public async Task<Result<int>> CreateNewUserAsync(UserModel usermodel)
        {
            if (usermodel == null) return Error.Validation("Brugermodellen er påkrævet");

            if (string.IsNullOrWhiteSpace(usermodel.UserName))
                return Error.Validation("Brugernavnet er påkrævet");

            if (string.IsNullOrWhiteSpace(usermodel.UserPassword))
                return Error.Validation("Kodeordet er påkrævet");

            try
            {
                // Tjekker om brugernavnet allerede er i brug
                var existing = await _userRepository.GetUserByUserName(usermodel.UserName);
                if (existing != null)
                {
                    return Error.Validation("Brugernavnet er allerede i brug");
                }

                // Hasher kodeordet før det gemmes
                var hashed = _passwordHasher.HashPassword(usermodel, usermodel.UserPassword);
                usermodel.UserPassword = hashed;

                var newUserId = await _userRepository.CreateUser(usermodel);
                return Result.Ok(newUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kunne ikke oprette bruger {UserName}", usermodel.UserName);
                return Error.Technical("Kunne ikke oprette bruger");
            }
        }

        public async Task<Result> UpdateUserAsync(UserModel usermodel)
        {
            if (usermodel == null) return Error.Validation("Brugermodellen er påkrævet");
            if (usermodel.UserID <= 0) return Error.Validation("Ugyldigt bruger-id");

            try
            {
                await _userRepository.UpdateUser(usermodel.UserID, usermodel);
                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Brugeren blev ikke fundet");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kunne ikke opdatere bruger med id {UserName}", usermodel.UserID);
                return Error.Technical("Kunne ikke opdatere brugeren");
            }
        }

        public async Task<Result> ValidateUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Error.Validation("Udfyld brugernavn og kodeord");
            }

            try
            {
                var user = await _userRepository.GetUserByUserName(username);

                if (user == null)
                {
                    return Error.Validation("Brugernavnet eller kodeordet er forkert");
                }

                var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.UserPassword, password);
                if (verifyResult != PasswordVerificationResult.Success)
                {
                    return Error.Validation("Brugernavn eller kodeordet er forkert");
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved validering af bruger {UserName}", username);
                return Error.Technical("Fejl ved validering af bruger");
            }
        }
    }
}
        