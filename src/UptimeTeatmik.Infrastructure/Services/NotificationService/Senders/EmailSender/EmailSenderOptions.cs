namespace UptimeTeatmik.Infrastructure.Services.NotificationService.Senders.EmailSender;

public class EmailSenderSettings
{
    public const string SectionName = "EmailSenderSettings";
    public string EmailApiUrl { get; set; } = null!;
    public string FromAddress { get; set; } = null!;
}