using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Interfaces;

namespace OverlapssytemApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserModel> UserModels { get; private set; } = new();
        public UserModel newUserModel { get; set; } = new UserModel
        {
            UserRole = UserRole.Medarbejder
        };

        //Hent alle
        public async Task<Result<List<UserModel>>> GetAllUsersAsync()
        {
            try
            {
                var data = await _userRepository.GetAllUsers();

                UserModels = data ?? new List<UserModel>();
                return UserModels;
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke hente bruger");
            }
        }

        //Hent på ID
        public async Task<Result<UserModel>> GetUserByIdAsync(int userID)
        {
            try
            {
                var task = await _userRepository.GetUserByID(userID);

                if (task == null) return Error.NotFound("Brugeren blev ikke fundet");

                return task;
            }
            catch (Exception)
            {
                return Error.Technical("Fejl ved indlæsning af bruger");
            }

        }

        //Slet
        public async Task<Result> DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Brugeren findes ikke");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke slette Brugeren");
            }
        }

        //Tilføj ny
        public async Task<Result<int>> CreateNewUserAsync(UserModel usermodel)
        {
            if (string.IsNullOrWhiteSpace(usermodel.UserName))
                return Error.Validation("Brugernavnet er påkrævet");

            if (string.IsNullOrWhiteSpace(usermodel.UserPassword))
                return Error.Validation("Kodeordet er påkrævet");
            
            try
            {
                var newUser = await _userRepository.CreateUser(usermodel);
                return Result.Ok(newUser);
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke oprette bruger");
            }
        }

        // Update
        public async Task<Result> UpdateUserAsync(UserModel usermodel)
        {
            try
            {
                await _userRepository.UpdateUser(usermodel.UserID, usermodel);
                return Result.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Error.NotFound("Brugeren blev ikke fundet");
            }
            catch (Exception)
            {
                return Error.Technical("Kunne ikke opdaterer brugeren");
            }
        }

        
    }
}
