using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterService(HttpClient httpClient, IOptions<BusinessRegisterSettings> settings) : IBusinessRegisterService
{
    public async Task<List<Business>> FetchUpdates(DateTime date)
    {
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

        response.EnsureSuccessStatusCode();

        var contentAsString = await response.Content.ReadAsStringAsync();
        var doc = XDocument.Parse(contentAsString);

        var ns = "{http://arireg.x-road.eu/producer/}";
        List<Business> businesses = [];

        foreach (var element in doc.Descendants(ns + "ettevotja_muudatused"))
        {
            var businessCode = element.Element(ns + "ariregistri_kood")?.Value;
            var businessName = element.Element(ns + "arinimi")?.Value;

            if (businessCode == null) continue;
            var business = new Business()
            {
                BusinessId = Guid.NewGuid(),
                BusinessName = businessName,
                BusinessCode = businessCode
            };
            businesses.Add(business);
        }
        
        return businesses;
    }
}