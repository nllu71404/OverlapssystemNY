using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemDomain.Interfaces
{
    public interface IUserRepository
    {
            
            Task<List<UserModel>> GetAllUsers();
            Task<UserModel> GetUserByID(int userID);
            Task<int> CreateUser(UserModel usermodel);
            Task UpdateUser(int userID, UserModel usermodel);
            Task DeleteUser(int userID);
    }
}
