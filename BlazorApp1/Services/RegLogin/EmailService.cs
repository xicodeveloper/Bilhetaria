
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Text;
using System.Threading.Tasks;
using BlazorApp1.Services.DataBase.DBEntities;

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
     public async Task SendOrderConfirmationAsync(Order order, string email)
{
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("Sua Loja", _smtpUser));
    message.To.Add(new MailboxAddress("Cliente", email));
    message.Subject = $"Recibo da Compra #{order.Id}";

    var bodyBuilder = new BodyBuilder();
    
    // Parte em HTML
    bodyBuilder.HtmlBody = BuildOrderHtml(order);
    
    // Parte em texto simples como fallback
    bodyBuilder.TextBody = BuildOrderText(order);

    message.Body = bodyBuilder.ToMessageBody();

    using (var client = new SmtpClient())
    {
        try
        {
            await client.ConnectAsync(
                _smtpServer,
                _smtpPort,
                MailKit.Security.SecureSocketOptions.SslOnConnect
            );

            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(message);
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

private string BuildOrderHtml(Order order)
{
    var sb = new StringBuilder();
    sb.AppendLine("<html><body>");
    sb.AppendLine("<h1 style='color: #2c3e50;'>Recibo de Compra</h1>");
    sb.AppendLine($"<p><strong>Data:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>");
    
    if (order.ShippingAddress != null)
    {
        sb.AppendLine("<h2 style='color: #34495e;'>Morada de Envio</h2>");
        sb.AppendLine($"<p>{order.ShippingAddress.Street} {order.ShippingAddress.Number}<br>");
        sb.AppendLine($"{order.ShippingAddress.ZipCode} {order.ShippingAddress.City}<br>");
        sb.AppendLine($"{order.ShippingAddress.Country}</p>");
    }

    sb.AppendLine("<h2 style='color: #34495e;'>Itens da Compra</h2>");
    sb.AppendLine("<table style='width: 100%; border-collapse: collapse;'>");
    sb.AppendLine("<tr style='background-color: #f8f9fa;'>");
    sb.AppendLine("<th style='padding: 10px; text-align: left;'>Produto</th>");
    sb.AppendLine("<th style='padding: 10px; text-align: left;'>Quantidade</th>");
    sb.AppendLine("<th style='padding: 10px; text-align: right;'>Preço</th></tr>");

    foreach (var item in order.Items)
    {
        sb.AppendLine("<tr style='border-bottom: 1px solid #dee2e6;'>");
        sb.AppendLine($"<td style='padding: 10px;'>{item.Movie.MovieTitle}</td>");
        sb.AppendLine($"<td style='padding: 10px;'>{item.Quantity}</td>");
        sb.AppendLine($"<td style='padding: 10px; text-align: right;'>{item.TotalPrice:0.00}€</td>");
        sb.AppendLine("</tr>");
    }

    sb.AppendLine("<tr style='font-weight: bold;'>");
    sb.AppendLine("<td colspan='2' style='padding: 10px; text-align: right;'>Total:</td>");
    sb.AppendLine($"<td style='padding: 10px; text-align: right;'>{order.Items.Sum(i => i.TotalPrice):0.00}€</td>");
    sb.AppendLine("</tr></table>");
    sb.AppendLine("</body></html>");

    return sb.ToString();
}

private string BuildOrderText(Order order)
{
    var sb = new StringBuilder();
    sb.AppendLine("Recibo de Compra");
    sb.AppendLine($"Data: {DateTime.Now:dd/MM/yyyy HH:mm}");
    
    if (order.ShippingAddress != null)
    {
        sb.AppendLine("\nMorada de Envio:");
        sb.AppendLine($"{order.ShippingAddress.Street} {order.ShippingAddress.Number}");
        sb.AppendLine($"{order.ShippingAddress.ZipCode} {order.ShippingAddress.City}");
        sb.AppendLine(order.ShippingAddress.Country);
    }

    sb.AppendLine("\nItens da Compra:");
    foreach (var item in order.Items)
    {
        sb.AppendLine($"{item.Movie.MovieTitle} - {item.Quantity}x {item.TotalPrice:0.00}€");
    }

    sb.AppendLine($"\nTotal: {order.Items.Sum(i => i.TotalPrice):0.00}€");
    return sb.ToString();
}

    }
}
