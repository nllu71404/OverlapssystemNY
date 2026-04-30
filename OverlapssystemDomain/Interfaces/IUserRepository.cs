using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IUserRepository
    {
            
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserByID(string userId);
        Task<UserModel> GetUserByUserName(string userName);
        Task<IdentityResult> CreateUser(UserModel userModel, string password, string role);
        Task<IdentityResult> UpdateUser(string userId, UserModel userModel);

        Task<IdentityResult> DeleteUser(string userId);
        Task<IdentityResult> AddToRoleAsync(UserModel user, string role);

    }
}
