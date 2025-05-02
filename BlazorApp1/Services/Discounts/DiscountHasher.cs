using System.Security.Cryptography;
using System.Text;

public static class DiscountHasher
{
    private static readonly string SecretKey = "ChaveSuperSecreta123!";

    public static string GenerateHash(int movieId, params string[] parameters)
    {
        // 1. Ordenar parâmetros alfabeticamente
        var orderedParams = parameters.OrderBy(p => p).ToArray();
        
        // 2. Criar payload consistente
        var payload = $"{movieId}|{string.Join("|", orderedParams)}|{SecretKey}";
        
        // 3. Normalizar encoding
        var payloadBytes = Encoding.UTF8.GetBytes(payload.Trim().ToLowerInvariant());
        
        using var sha = SHA256.Create();
        var hashBytes = sha.ComputeHash(payloadBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    public static bool ValidateHash(int movieId, string[] parameters, string providedHash)
    {
        var generatedHash = GenerateHash(movieId, parameters);
        return generatedHash.Equals(providedHash, StringComparison.OrdinalIgnoreCase);
    }
}