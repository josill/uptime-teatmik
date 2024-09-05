using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Domain;
using UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterService(IAppDbContext dbContext, HttpClient httpClient, IOptions<BusinessRegisterSettings> settings, IBusinessRegisterBodyGenerator businessRegisterBodyGenerator) : IBusinessRegisterService
{
    public async Task RunBusinessUpdateJob()
    {
        var dateNow = DateTime.Now;
        await FetchUpdatedBusinessCodesAsync(dateNow);
    }
    
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
                // await UpdateBusinessAsync(businessCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update business with code {businessCode} with exception: {ex}");
            }
        }
    }
    //
    // private async Task UpdateBusinessAsync(string businessCode)
    // {
    //     var body = businessRegisterBodyGenerator.GenerateDetailDataUrlXmlBody(businessCode);
    //     var responseContent = await GetXmlResponseContentAsync(body, settings.Value.DetailDataUrl);
    //     try
    //     {
    //         var formattedJson = BusinessRegisterParser.ParseBusinessFormattedJson(responseContent);
    //         var businessName = BusinessRegisterParser.ParseBusinessName(responseContent);
    //         var relatedPersons = await UpdateBusinessRelatedPersons(responseContent);
    //         
    //         // TODO: Correctly handle the error
    //         if (businessName == null) throw new InvalidOperationException("Error parsing business name");
    //         
    //         var updatedBusiness = await dbContext.Businesses
    //             .Where(b => b.BusinessCode == businessCode)
    //             .FirstOrDefaultAsync();
    //         var updatedBre =
    //             await dbContext.BusinessRegisterEntities.FirstOrDefaultAsync(b =>
    //                 b.BusinessOrPersonalCode == businessCode);
    //         if (updatedBre != null)
    //         {
    //             updatedBre.BusinessOrLastName = businessName;
    //             updatedBre.FormattedJson = formattedJson;
    //         }
    //         else
    //         {
    //             
    //         }
    //         
    //         if (updatedBusiness != null)
    //         {
    //             updatedBusiness.BusinessName = businessName;
    //             updatedBusiness.FormattedJson = formattedJson;
    //
    //             dbContext.Businesses.Update(updatedBusiness);
    //         }
    //         else
    //         {
    //             updatedBusiness = new Business()
    //             {
    //                 BusinessId = Guid.NewGuid(),
    //                 BusinessCode = businessCode,
    //                 BusinessName = businessName,
    //                 FormattedJson = formattedJson
    //             };
    //             dbContext.Businesses.Add(updatedBusiness);
    //         }
    //
    //         await dbContext.SaveChangesAsync();
    //     }
    //     catch (JsonException)
    //     {
    //         // TODO: Correctly handle the error
    //     }
    // }
    //
    //   
    // private async Task<List<Person>> UpdateBusinessRelatedPersons(string responseContent)
    // {
    //     var result = new List<Person>();
    //     var relatedPersons = BusinessRegisterParser.ParseBusinessRelatedPersons(responseContent);
    //     if (relatedPersons == null) return result;
    //     
    //     foreach (var person in relatedPersons)
    //     {
    //         var parsedPerson = new ParsedPerson(person);
    //     }
    //
    //     return result;
    // }
    

    private async Task<string> GetXmlResponseContentAsync(string body, string endPointUrl)
    {
        var content = new StringContent(body, Encoding.UTF8, "text/xml");
        var response = await httpClient.PostAsync(endPointUrl, content);
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}