using System.Security.Cryptography;
using System.Text;

public static class DiscountHasher
{
    private static readonly string SecretKey = "ChaveSuperSecreta123!"; // guarda isto fora do código-fonte idealmente

    public static string GenerateHash(int movieId, string discount)
    {
        string data = $"{movieId}:{discount}:{SecretKey}";
        using var sha = SHA256.Create();
        var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToHexString(hashBytes); // .NET 5+
    }

    public static bool ValidateHash(int movieId, string discount, string providedHash)
    {
        string expectedHash = GenerateHash(movieId, discount);
        return string.Equals(providedHash, expectedHash, StringComparison.OrdinalIgnoreCase);
    }
}