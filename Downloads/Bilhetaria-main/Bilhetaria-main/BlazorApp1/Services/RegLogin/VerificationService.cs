using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;


namespace BlazorApp1.Services.RegLogin
{
    public class VerificationService
    {
        private readonly EmailService _emailService;
        private static ConcurrentDictionary<string, string> _codes = new ConcurrentDictionary<string, string>();

        public VerificationService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<bool> SendVerificationCodeAsync(string email)
        {
            var code = new Random().Next(100000, 999999).ToString(); // Gera código de 6 dígitos
            _codes[email] = code;
            await _emailService.SendVerificationCodeAsync(email, code);
            return true;
        }

        public bool ValidateCode(string email, string code)
        {
            if (_codes.TryGetValue(email, out var storedCode) && storedCode == code)
            {
                _codes.TryRemove(email, out _); // Remove o código após validação
                return true;
            }
            return false;
        }
    }
}