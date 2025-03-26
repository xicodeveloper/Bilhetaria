using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Services.RegLogin
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthStateProvider(
            IAuthService authService,
            IAuthenticationService authenticationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            
            var principal = context?.User ?? new ClaimsPrincipal();
        
            return new AuthenticationState(principal);
        }

        public async Task UpdateAuthenticationStateAsync(ClaimsPrincipal principal)
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                
                if (principal?.Identity?.IsAuthenticated == true)
                {
                    await _authenticationService.SignInAsync(
                        context,
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                        });
                }
                else
                {
                    await _authenticationService.SignOutAsync(
                        context,
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new AuthenticationProperties());
                }
                
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar estado: {ex}");
                throw;
            }
        }
    }
}