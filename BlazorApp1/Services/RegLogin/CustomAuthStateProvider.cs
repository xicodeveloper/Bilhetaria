using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Services.RegLogin
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            
            // Obter o principal atual sem validação adicional
            var principal = context?.User ?? new ClaimsPrincipal();
            
            // Criar novo estado de autenticação sem verificar confirmação
            return new AuthenticationState(principal);
        }

        public async Task UpdateAuthenticationStateAsync(ClaimsPrincipal principal)
        {
            var context = _httpContextAccessor.HttpContext;
            
            if (principal?.Identity?.IsAuthenticated == true)
            {
                // Manter apenas claims essenciais
                var cleanClaims = new List<Claim>
                {
                    principal.FindFirst(ClaimTypes.NameIdentifier),
                    principal.FindFirst(ClaimTypes.Name),
                    principal.FindFirst(ClaimTypes.Email)
                }.Where(c => c != null).ToList();

                var identity = new ClaimsIdentity(cleanClaims, "Cookies");
                var cleanPrincipal = new ClaimsPrincipal(identity);

                await context.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    cleanPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                    });
            }
            else
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}