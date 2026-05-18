using Overlapssystem.ViewModels;
using OverlapssystemShared;

namespace Overlapssystem.Interfaces
{
    public interface IUserFacade
    {
        Task AddUser(UserViewModel vm);
        Task<List<UserDTO>> GetAllUsers();
        Task DeleteUser(string userId);
        Task UpdateUser(string userId, UserViewModel vm);
        Task<string?> ValidateUser(UserViewModel vm);
    }
}
