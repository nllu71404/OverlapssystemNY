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

        public UserFacade (UserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public async Task<Result<int>> AddUser(UserViewModel vm)
        {
            var dto = MapAddUser(vm);
            var result = await _userApiService.CreateUser(dto);
            return result;
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
