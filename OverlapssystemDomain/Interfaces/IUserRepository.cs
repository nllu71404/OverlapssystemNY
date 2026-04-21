using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlapssystemDomain.Interfaces
{
    public interface IUserRepository
    {
            public bool CreateUser(string userName, string userPassword, Enums.UserRole userRole);
            public List<Entities.UserModel> GetAllUsers();
            public Entities.UserModel GetUserByID(int userID);
            public bool UpdateUser(int userID, string userName, string userPassword, Enums.UserRole userRole);
            public bool DeleteUser(int userID);
    }
}
