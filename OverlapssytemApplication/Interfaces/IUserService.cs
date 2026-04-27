using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IUserService
    {
        Task<Result<List<UserModel>>> GetAllUsersAsync();
        Task<Result<UserModel>> GetUserByIdAsync(string userId);
        Task<Result<UserModel>> GetUserByUserNameAsync(string userName);
        Task<Result> CreateNewUserAsync(UserModel usermodel, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<Result> UpdateUserAsync(string userId, UserModel userModel);
    }
}
