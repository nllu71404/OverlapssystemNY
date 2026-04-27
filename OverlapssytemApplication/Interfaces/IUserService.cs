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
        Task<Result<UserModel>> GetUserByIdAsync(int userID);
        Task<Result<UserModel>> GetUserByUserNameAsync(string userName);
        Task<Result<int>> CreateNewUserAsync(UserModel usermodel);
        Task<Result> DeleteUserAsync(int id);
        Task<Result> UpdateUserAsync(UserModel usermodel);
        Task<Result> ValidateUserAsync(string userName, string password);
    }
}
