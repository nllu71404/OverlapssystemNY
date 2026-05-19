using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OverlapssytemApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository, IDepartmentRepository departmentRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
        }
            

        public async Task<Result<List<UserModel>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return Result.Ok(users ?? new List<UserModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all users");
                return Error.Technical("Kunne ikke hente brugere");
            }
        }

        public async Task<Result<UserModel>> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return Error.Validation("Ugyldigt bruger-id");

            try
            {
                var user = await _userRepository.GetUserByID(userId);
                if (user == null) return Error.NotFound("Brugeren blev ikke fundet");
                return Result.Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved indlæsning af bruger med id {UserId}", userId);
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

        public async Task<Result> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return Error.Validation("Ugyldigt bruger-id");

            try
            {
                var result = await _userRepository.DeleteUser(userId);
                if (!result.Succeeded)
                    return Error.NotFound("Brugeren findes ikke");
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kunne ikke slette bruger med id {UserId}", userId);
                return Error.Technical("Kunne ikke slette brugeren");
            }
        }

        //Metodens navn viser os at den returnerer et resultat (fejl eller succes) 
        public async Task<Result> CreateNewUserAsync(UserModel userModel, string password, string role)
        {

            //Her kaster vi en ArgumentNullException hvis userModel er null, da dette ikke er en forventet situation 
            //I dette tilfælde er det bedre at fejle hurtigt end at fortsætte med en ugyldig tilstand
            ArgumentNullException.ThrowIfNull(userModel);

            //Her kaster vi IKKE en exception for ugyldigt input, da dette er en forventelig fejl.
            //Tjekker om det valgte afdelings-id findes, så der ikke oprettes en bruger der er tilknyttet en ugyldig afdeling. 
            if (!userModel.DepartmentId.HasValue)
                return Error.Validation("Afdeling er påkrævet");

            var exists = await _departmentRepository.ExistsAsync(userModel.DepartmentId.Value);

            if (!exists)
                return Error.Validation("Den valgte afdeling findes ikke");


            try
            {
 
                //Identity har indbygget validering for forventelige fejl som duplikate brugernavne og svage adgangskoder
                //Vi sætter selv fejlmeddelelserne i denne metode, så de er på dansk og mere brugervenlige.
                var result = await _userRepository.CreateUser(userModel, password, role);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(error => error.Code switch
                    {
                        "DuplicateUserName" => "Brugernavnet er allerede i brug",
                        "PasswordTooShort" => "Adgangskoden er for kort",
                        "PasswordRequiresNonAlphanumeric" =>
                            "Adgangskoden skal indeholde et specialtegn",
                        "PasswordRequiresDigit" =>
                            "Adgangskoden skal indeholde mindst ét tal",
                        "PasswordRequiresUpper" =>
                            "Adgangskoden skal indeholde mindst ét stort bogstav",
                        _ => "Brugeren kunne ikke oprettes"
                    });

                    return Error.Validation(string.Join(", ", errors));
                }

                
                // Tildel Identity rolle
                await _userRepository.AddToRoleAsync(userModel, role);

                // Hvis alt lykkedes, returner en succes.
                return Result.Ok();
            }
            catch (Exception ex)
            {
                // Hvis der opstår en uventet fejl, logges den med detaljeret fejlmeddelse til udviklere.
                // Vi returner en generisk teknisk fejlmeddelelse til brugeren, for at undgå at eksponere følsomme detaljer.
                _logger.LogError(ex, "Kunne ikke oprette bruger {UserName}", userModel.UserName);
                return Error.Technical("Kunne ikke oprette bruger");
            }
        }

        public async Task<Result> UpdateUserAsync(string userId, UserModel userModel)
        {
            if (string.IsNullOrWhiteSpace(userId)) return Error.Validation("Ugyldigt bruger-id");
            if (userModel == null) return Error.Validation("Brugerrollen er påkrævet");

            try
            {
                var result = await _userRepository.UpdateUser(userId, userModel);
                if (!result.Succeeded)
                    return Error.NotFound("Brugeren blev ikke fundet");
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kunne ikke opdatere bruger med id {UserName}", userId);
                return Error.Technical("Kunne ikke opdatere brugeren");
            }
        }        
    }
}
        