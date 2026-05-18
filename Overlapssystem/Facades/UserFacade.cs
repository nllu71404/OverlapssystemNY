using Overlapssystem.Interfaces;
using Overlapssystem.Services;
using Overlapssystem.ViewModels;
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

        public async Task AddUser(UserViewModel vm)
        {
            var dto = MapAddUser(vm);
            await _userApiService.CreateUser(dto);
        }
        public async Task<List<UserDTO>> GetAllUsers()
        {
            return await _userApiService.GetAllUsers();
        }

        public async Task DeleteUser(string userId)
        {
            await _userApiService.DeleteUser(userId);
        }
        public async Task UpdateUser(string userId, UserViewModel vm)
        {
            var dto = new UpdateUserDTO
            {
                UserName = vm.UserName,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DepartmentId = vm.DepartmentId
            };

            await _userApiService.UpdateUser(userId, dto);
        }
        public async Task<string?> ValidateUser(UserViewModel vm)
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
    }
}
