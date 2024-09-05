using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser;

public class BusinessRegisterParser
{
    
    public static JToken? ParseBusinessRelatedPersons(string responseContent)
    {
        var jObject = JObject.Parse(responseContent);
        var relatedPersons = jObject["keha"]?["ettevotjad"]?["item"]?[0]?["isikuandmed"]?["kaardile_kantud_isikud"]?["item"];

        return relatedPersons;
    }

    public static string? ParseBusinessRelatedPerson(JToken personJson)
    {
        var personPersonalOrBusinessCode = personJson["isikukood_registrikood"].ToString();
        var firstName = personJson["eesnimi"].ToString();
        var lastOrBusinessName = personJson["nimi_arinimi"].ToString();
        var personType = personJson["isiku_roll"].ToString();
        var json = JsonConvert.DeserializeObject(personJson.ToString());
        var formattedJson = JsonConvert.SerializeObject(json, Formatting.Indented);

        return "";
    }

    public static string? ParseBusinessName(string responseContent)
    {
        var jObject = JObject.Parse(responseContent);
        var businessName = jObject["keha"]?["ettevotjad"]?["item"]?[0]?["nimi"].ToString();

        return businessName;
    }

    public static string ParseBusinessFormattedJson(string responseContent)
    {
        var jsonObject = JsonConvert.DeserializeObject(responseContent);
        var formattedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

        return formattedJson;
    }
}