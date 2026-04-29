using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Interfaces;

namespace OverlapssystemInfrastructure.Service
{
    public class AuthService : IAuthService
    {
        
        private readonly UserManager<UserModel> _userManager;
        private readonly JwtService _jwtService;

        public AuthService(UserManager<UserModel> userManager, JwtService jwtService)
        {
            
            _userManager = userManager;
            _jwtService = jwtService;
        }

        // SignInManager validerer password og JWTservice genererer tokenet
        public async Task<Result<string>> LoginAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return Error.Validation("Udfyld brugernavn og kodeord");

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return Error.Validation("Brugernavn eller kodeord er forkert");

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
                return Error.Validation("Brugernavn eller kodeord er forkert");

            // Hent brugerens Identity roller
            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user, roles);
            return Result.Ok(token);
        }

        // SignInManager håndterer logout og afslutter brugerens session
        public Task<Result> LogoutAsync()
        {
            return Task.FromResult(Result.Ok());
        }
    }
}
