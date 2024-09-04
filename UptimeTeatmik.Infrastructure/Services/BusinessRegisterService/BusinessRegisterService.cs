using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UptimeTeatmik.Application.Common;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterService(IAppDbContext dbContext, HttpClient httpClient, IOptions<BusinessRegisterSettings> settings) : IBusinessRegisterService
{
    public async Task<List<string>> FetchUpdatedBusinessCodesAsync(DateTime date)
    {
        var body = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xro=""http://x-road.eu/xsd/xroad.xsd"" xmlns:iden=""http://x-road.eu/xsd/identifiers"" xmlns:prod=""http://arireg.x-road.eu/producer/"">
            <soapenv:Body>
                <prod:ettevotjaMuudatusedTasuline_v1>
                    <prod:keha>
                        <prod:ariregister_kasutajanimi>{settings.Value.Username}</prod:ariregister_kasutajanimi>
                        <prod:ariregister_parool>{settings.Value.Password}</prod:ariregister_parool>
                        <prod:ariregistri_kood>70000310</prod:ariregistri_kood>
                        <prod:kuupaev>{date:yyyy-MM-dd}</prod:kuupaev>
                    </prod:keha>
                </prod:ettevotjaMuudatusedTasuline_v1>
            </soapenv:Body>
        </soapenv:Envelope>";
        var responseContent = await GetXmlResponseContentAsync(body, settings.Value.ChangesUrl);
        
        var doc = XDocument.Parse(responseContent);
        var ns = "{http://arireg.x-road.eu/producer/}";
        List<string> businessCodes = [];

        foreach (var element in doc.Descendants(ns + "ettevotja_muudatused"))
        {
            var businessCode = element.Element(ns + "ariregistri_kood")?.Value;

            if (businessCode == null) continue;
            businessCodes.Add(businessCode);
        }
        
        return businessCodes;
    }

    public async Task UpdateBusinessesAsync(List<string> businessesCodes)
    {
        foreach (var businessCode in businessesCodes)
        {
            try
            {
                await UpdateBusinessAsync(businessCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update business with code {businessCode} with exception: {ex}");
            }
            
            break;
        }
    }

    private async Task UpdateBusinessAsync(string businessCode)
    {
        var body = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xro=""http://x-road.eu/xsd/xroad.xsd"" xmlns:iden=""http://x-road.eu/xsd/identifiers"" xmlns:prod=""http://arireg.x-road.eu/producer/"">
                <soapenv:Body>
                 <prod:detailandmed_v2>
                     <prod:keha>
                         <prod:ariregister_kasutajanimi>{settings.Value.Username}</prod:ariregister_kasutajanimi>
                         <prod:ariregister_parool>{settings.Value.Password}</prod:ariregister_parool>
                         <prod:ariregistri_kood>{businessCode}</prod:ariregistri_kood> 
                         <prod:ariregister_valjundi_formaat>json</prod:ariregister_valjundi_formaat>
                         <prod:yandmed>1</prod:yandmed>
                         <prod:iandmed>1</prod:iandmed>
                         <prod:kandmed>0</prod:kandmed>
                         <prod:dandmed>0</prod:dandmed>
                         <prod:maarused>0</prod:maarused>
                     </prod:keha>
                 </prod:detailandmed_v2>
                </soapenv:Body>
            </soapenv:Envelope>";
        var responseContent = await GetXmlResponseContentAsync(body, settings.Value.DetailDataUrl);
        try
        {
            var formattedJson = ParseBusinessFormattedJson(responseContent);
            var businessName = ParseBusinessName(responseContent);
            // TODO: Correctly handle the error
            if (businessName == null) throw new InvalidOperationException("Error parsing business name");
            
            // TODO: update business
            var updatedBusiness = await dbContext.Businesses
                .Where(b => b.BusinessCode == businessCode)
                .FirstOrDefaultAsync();
            if (updatedBusiness != null)
            {
                updatedBusiness.BusinessName = businessName;
                updatedBusiness.FormattedJson = formattedJson;

                dbContext.Businesses.Update(updatedBusiness);
            }
            else
            {
                updatedBusiness = new Business()
                {
                    BusinessId = Guid.NewGuid(),
                    BusinessCode = businessCode,
                    BusinessName = businessName,
                    FormattedJson = formattedJson
                };
                dbContext.Businesses.Add(updatedBusiness);
            }

            await dbContext.SaveChangesAsync();
        }
        catch (JsonException)
        {
            // TODO: Correctly handle the error
        }
    }

    private async Task<string> GetXmlResponseContentAsync(string body, string endPointUrl)
    {
        var content = new StringContent(body, Encoding.UTF8, "text/xml");
        var response = await httpClient.PostAsync(endPointUrl, content);
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    private static string? ParseBusinessName(string responseContent)
    {
        var jObject = JObject.Parse(responseContent);
        var businessName = jObject["keha"]?["ettevotjad"]?["item"]?[0]?["nimi"].ToString();

        return businessName;
    }

    private static string ParseBusinessFormattedJson(string responseContent)
    {
        var jsonObject = JsonConvert.DeserializeObject(responseContent);
        var formattedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

        return formattedJson;
    }
}