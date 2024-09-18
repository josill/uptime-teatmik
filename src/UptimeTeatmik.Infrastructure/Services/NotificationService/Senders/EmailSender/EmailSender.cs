using System.Net.Http;
using System.Text;
using System.Text.Json;
using UptimeTeatmik.Application.Common.Interfaces.NotificationService;

namespace UptimeTeatmik.Infrastructure.Services.NotificationService.Senders.EmailSender;

public class EmailSender(HttpClient httpClient, EmailSenderSettings settings) : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = false)
    {
        var emailRequest = new
        {
            To = new[] { new { Email = to } },
            Subject = subject,
            HTML = isBodyHtml ? body : null,
            Text = !isBodyHtml ? body : null,
            From = new { Email = settings.FromAddress },
        };

        var json = JsonSerializer.Serialize(emailRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(settings.EmailApiUrl, content);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
        }
    }
}