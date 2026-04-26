using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemDomain.Interfaces
{
    public interface IUserRepository
    {
            Task CreateUser(string userName, string userPassword, Enums.UserRole userRole);
            Task<List<Entities.UserModel>> GetAllUsers();
            Task<Entities.UserModel> GetUserByID(int userID);
            Task UpdateUser(int userID, string userName, string userPassword, Enums.UserRole userRole);
            Task DeleteUser(int userID);
    }
}
