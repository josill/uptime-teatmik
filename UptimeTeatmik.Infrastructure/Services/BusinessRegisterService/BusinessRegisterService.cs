using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UptimeTeatmik.Application.Common;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterService(IAppDbContext dbContext, HttpClient httpClient, IOptions<BusinessRegisterSettings> settings, IBusinessRegisterBodyGenerator businessRegisterBodyGenerator) : IBusinessRegisterService
{
    public async Task<List<string>> FetchUpdatedBusinessCodesAsync(DateTime date)
    {
        var body = businessRegisterBodyGenerator.GenerateChangesUrlXmlBody(date);
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
        var body = businessRegisterBodyGenerator.GenerateDetailDataUrlXmlBody(businessCode);
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