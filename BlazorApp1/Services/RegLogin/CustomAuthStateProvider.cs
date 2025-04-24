using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task RefreshAuthenticationStateAsync()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            await context.AuthenticateAsync(); // Força a recarregar o estado
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();
        var context = _httpContextAccessor.HttpContext;
        
        if (context != null)
        {
            // Forçar a recriação do contexto de autenticação
            await context.AuthenticateAsync();
            principal = context.User;
        }
        
        return new AuthenticationState(principal);
    }

    public async Task UpdateAuthenticationStateAsync(ClaimsPrincipal principal)
    {
        var context = _httpContextAccessor.HttpContext;
        
        if (context != null)
        {
            if (principal?.Identity?.IsAuthenticated == true)
            {
                await context.SignInAsync(
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
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
        
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}