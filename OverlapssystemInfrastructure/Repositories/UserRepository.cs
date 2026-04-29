using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Enums;
using OverlapssystemDomain.Interfaces;
using OverlapssystemInfrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace OverlapssystemInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        //Bruger UserManager fra ASP.NET Identity (indbygget klasse) til at håndtere brugere
        private readonly UserManager<UserModel> _userManager;

        public UserRepository(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        // CreateAsync opretter brugeren i databasen og hasher passwordet automatisk
        // Vi sender password som separat parameter da Identity aldrig gemmer det i plaintext
        public async Task<IdentityResult> CreateUser(UserModel userModel, string password, string role)
        {
            return await _userManager.CreateAsync(userModel, password);
        }

        // FindByIdAsync finder brugeren via Identity's eget Id (string/GUID)
        // Vi bruger ikke længere int UserID da Identity genererer sit eget unikke Id
        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await GetUserByID(userId);
            return await _userManager.DeleteAsync(user);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<UserModel> GetUserByID(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<UserModel> GetUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        // Vi opdaterer ikke password her - det håndteres separat via Identity's ChangePasswordAsync
        // IdentityResult returneres så kalderen kan se om opdateringen lykkedes eller fejlede
        public async Task<IdentityResult> UpdateUser(string userId, UserModel userModel)
        {
            var user = await GetUserByID(userId);
            user.UserName = userModel.UserName;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.DepartmentId = userModel.DepartmentId;
            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> AddToRoleAsync(UserModel user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

    }

}
