using Overlapssystem.Interfaces;
using Overlapssystem.Services;
using Overlapssystem.ViewModels;
using OverlapssytemApplication.Common.Result;
using OverlapssystemShared;

namespace Overlapssystem.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly UserApiService _userApiService;

        public UserFacade(UserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public async Task<Result> AddUser(UserViewModel vm)
        {
            var dto = MapAddUser(vm);
            return await _userApiService.CreateUser(dto);

        }

        public async Task<Result<List<UserDTO>>> GetAllUsers()
        {
            return await _userApiService.GetAllUsers();
        }

        public async Task<Result> DeleteUser(string userId)
        {
            return await _userApiService.DeleteUser(userId);
        }

        public async Task<Result> UpdateUser(string userId, UserViewModel vm)
        {
            var dto = MapUpdateUser(vm);
            return await _userApiService.UpdateUser(userId, dto);
        }

        public async Task<Result<string?>> ValidateUser(UserViewModel vm)
        {
            return await _userApiService.ValidateUser(vm.UserName, vm.Password);
        }


        private AddUserDTO MapAddUser(UserViewModel vm)
        {
            return new AddUserDTO
            {
                UserName = vm.UserName,
                Password = vm.Password,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DepartmentId = vm.DepartmentId,
                Role = vm.Role
            };
        }

        private UpdateUserDTO MapUpdateUser(UserViewModel vm)
        {
            return new UpdateUserDTO
            {
                UserName = vm.UserName,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DepartmentId = vm.DepartmentId
            };
        }
    }
}
