using System.Security.Claims;

namespace BlazorApp1.Services.RegLogin
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public ClaimsPrincipal? ClaimsPrincipal { get; set; }
        public string? ErrorMessage { get; set; }
        
    }
}