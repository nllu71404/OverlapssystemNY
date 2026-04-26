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
        public async Task CreateUser(string userName, string userPassword, UserRole userRole)
        {
            var user = new UserModel
            {
                UserName = userName,
                UserPassword = userPassword,
                UserRole = userRole
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
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

        public Task UpdateUser(int userID, string userName, string userPassword, UserRole userRole)
        {
            var user = new UserModel
            {
                UserName = userName,
                UserPassword = userPassword,
                UserRole = userRole
            };
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

}
