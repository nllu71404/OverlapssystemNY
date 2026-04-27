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
        private readonly SignInManager<UserModel> _signInManager;

        public AuthService(SignInManager<UserModel> signInManager)
        {
            _signInManager = signInManager;
        }

        // SignInManager håndterer login og tjekker automatisk det hashede password
        public async Task<Result> LoginAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return Error.Validation("Udfyld brugernavn og kodeord");

            var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            if (!result.Succeeded)
                return Error.Validation("Brugernavn eller kodeord er forkert");

            return Result.Ok();
        }

        // SignInManager håndterer logout og afslutter brugerens session
        public async Task<Result> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Result.Ok();
        }
    }
}
