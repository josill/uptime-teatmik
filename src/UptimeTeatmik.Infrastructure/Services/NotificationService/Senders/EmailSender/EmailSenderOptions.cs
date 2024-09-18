namespace UptimeTeatmik.Infrastructure.Services.NotificationService.Senders.EmailSender;

public class EmailSenderSettings
{
    public const string SectionName = "EmailSenderSettings";
    public string SmtpHost { get; set; } = null!;
    public int SmtpPort { get; set; }
    public string FromAddress { get; set; } = null!;
}