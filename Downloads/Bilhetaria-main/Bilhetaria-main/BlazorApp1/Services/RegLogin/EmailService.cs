
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace BlazorApp1.Services.RegLogin
{
    public class EmailService
    {
        // Valide estas configurações no seu provedor de email
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 465;
        private readonly string _smtpUser = "forofrancisco4@gmail.com";
        private readonly string _smtpPass = "yesn rvvj uqhd beep"; // Considere usar Secret Manager
        public async Task SendVerificationCodeAsync(string email, string code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BlazorApp", _smtpUser));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Código de Verificação";
            message.Body = new TextPart("plain") { Text = $"Seu código de verificação é: {code}" };

            using (var client = new SmtpClient())
            {
                try
                {
                    // Configuração SSL correta para porta 465
                    await client.ConnectAsync(
                        _smtpServer, 
                        _smtpPort, 
                        MailKit.Security.SecureSocketOptions.SslOnConnect
                    );
            
                    await client.AuthenticateAsync(_smtpUser, _smtpPass);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    // Logar o erro e relançar para tratamento superior
                    Console.WriteLine($"Erro ao enviar email: {ex}");
                    throw new ApplicationException("Falha no envio do código de verificação", ex);
                }
                finally
                {
                    if (client.IsConnected)
                    {
                        await client.DisconnectAsync(true);
                    }
                }
            }
        }
    }
}
