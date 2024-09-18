using System.Net.Mail;
using UptimeTeatmik.Application.Common.Interfaces.NotificationService;

namespace UptimeTeatmik.Infrastructure.Services.NotificationService.Senders;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = false)
    {
        var mailMessage = new MailMessage
        {
            // TODO: inject email options
            // From = new MailAddress(_emailOptions.FromAddress),
            From = new MailAddress("sender@example.ee"),
            Subject = subject,
            Body = body,
            IsBodyHtml = isBodyHtml
        };
        mailMessage.To.Add(to);
        
        try
        {
            // TODO: inject smtp client
            // await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}