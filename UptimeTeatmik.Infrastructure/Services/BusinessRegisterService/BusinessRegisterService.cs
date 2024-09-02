using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using UptimeTeatmik.Application.Common.Interfaces;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterService(HttpClient httpClient, IOptions<BusinessRegisterSettings> settings) : IBusinessRegisterService
{
    public async Task<List<string>> FetchUpdates(DateTime date)
    {
        Console.WriteLine($"{date:yyyy-MM-dd}");
        Console.WriteLine($"{date:yyyy-MM-dd}");
        Console.WriteLine($"{date:yyyy-MM-dd}");
        Console.WriteLine($"{date:yyyy-MM-dd}");
        Console.WriteLine($"{date:yyyy-MM-dd}");
        var body = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xro=""http://x-road.eu/xsd/xroad.xsd"" xmlns:iden=""http://x-road.eu/xsd/identifiers"" xmlns:prod=""http://arireg.x-road.eu/producer/"">
         <soapenv:Body>
         <prod:ettevotjaMuudatusedTasuline_v1>
         <prod:keha>
          <prod:ariregister_kasutajanimi>{settings.Value.Username}</prod:ariregister_kasutajanimi>
         <prod:ariregister_parool>{settings.Value.Password}</prod:ariregister_parool>
         <prod:ariregistri_kood>70000310</prod:ariregistri_kood>
         <prod:ariregister_valjundi_formaat>json</prod:ariregister_valjundi_formaat>
         <prod:kuupaev>{date:yyyy-MM-dd}</prod:kuupaev>
         </prod:keha>
          </prod:ettevotjaMuudatusedTasuline_v1>
          </soapenv:Body>
        </soapenv:Envelope>";

        var content = new StringContent(body, Encoding.UTF8, "text/xml");
        var response = await httpClient.PostAsync(settings.Value.ChangesUrl, content);

            Console.WriteLine($"Error: {response.StatusCode}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return new List<string>();

        return new List<string>();
    }
}