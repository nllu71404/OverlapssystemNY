using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssytemApplication.Common;

namespace OverlapssytemApplication.Interfaces
{
    public interface IAuthService
    {
        // Login validerer brugernavn og password via Identity
        Task<Result<string>> LoginAsync(string userName, string password);

        // Logout afslutter brugerens session
        Task<Result> LogoutAsync();
    }
}
