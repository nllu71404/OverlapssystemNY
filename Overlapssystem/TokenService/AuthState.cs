using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Overlapssystem.TokenService
{
    public class AuthState : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthState(IJSRuntime js)
        {
            _js = js;
        }


        // Kaldes af Blazor for at få den aktuelle brugers authentication state
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Hent token fra sessionStorage ved hver side load
            try
            {
                var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "authToken");

                if (string.IsNullOrEmpty(token))
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                _currentUser = BuildClaimsPrincipal(token);
                return new AuthenticationState(_currentUser);
            }
            catch
            {
                // JS er ikke klar endnu under pre-rendering
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        // Kaldes når brugeren logger ind med et JWT token
        public async Task MarkUserAsAuthenticated(string token)
        {
            await _js.InvokeVoidAsync("sessionStorage.setItem", "authToken", token);
            _currentUser = BuildClaimsPrincipal(token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        // Kaldes når brugeren logger ud
        public async Task MarkUserAsLoggedOut()
        {
            await _js.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private ClaimsPrincipal BuildClaimsPrincipal(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims.Select(c =>
                c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                    ? new Claim(ClaimTypes.Role, c.Value)
                    : c).ToList();
            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }

        
        
    }
}
