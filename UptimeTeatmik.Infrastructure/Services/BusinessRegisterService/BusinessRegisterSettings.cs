namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterSettings
{
    public const string SectionName = "BusinessRegisterSettings";
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ChangesUrl { get; set; } = null!;
}