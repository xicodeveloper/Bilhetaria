using System.Security.Claims;

public class AuthResult
{
    public bool Success { get; set; }
    public ClaimsPrincipal? ClaimsPrincipal { get; set; }
}