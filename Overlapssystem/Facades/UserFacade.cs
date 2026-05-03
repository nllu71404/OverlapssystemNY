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
