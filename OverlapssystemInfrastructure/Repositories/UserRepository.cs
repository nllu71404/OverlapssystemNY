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

namespace OverlapssystemInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OverlapDbContext _context;
        public UserRepository(OverlapDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateUser(UserModel usermodel)
        {
            await _context.Users.AddAsync(usermodel);
            await _context.SaveChangesAsync();
            return usermodel.UserID;
        }

        public async Task DeleteUser(int userID)
        {
            var user = await GetUserByID(userID);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserByID(int userID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == userID);
        }

        public async Task<UserModel> GetUserByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task UpdateUser(int userID, UserModel usermodel)
        {
            var user = await GetUserByID(userID);

            user.UserName = usermodel.UserName;
            user.UserPassword = usermodel.UserPassword;
            user.UserRole = usermodel.UserRole;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        
    }

}
