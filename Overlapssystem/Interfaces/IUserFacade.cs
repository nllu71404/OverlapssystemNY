using Overlapssystem.ViewModels;
using OverlapssytemApplication.Common.Result;
using OverlapssystemShared;

namespace Overlapssystem.Interfaces
{
    public interface IUserFacade
    {
        Task<Result> AddUser(UserViewModel vm);
        Task<Result<List<UserDTO>>> GetAllUsers();
        Task<Result> DeleteUser(string userId);
        Task<Result> UpdateUser(string userId, UserViewModel vm);
        Task<Result<string?>> ValidateUser(UserViewModel vm);
    }
}
