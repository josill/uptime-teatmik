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

    public static ParsedEntity ParseEntity(string responseContent)
    {
        var jObject = JObject.Parse(responseContent);
        var entityJson = jObject["keha"]?["ettevotjad"]?["item"]?[0];
        if (entityJson == null) throw new InvalidOperationException("Unable to retrieve information about entity.");
        
        var jsonObject = JsonConvert.DeserializeObject(responseContent);
        var formattedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        
        var parsedEntity = new ParsedEntity(entityJson)
        {
            FormattedJson = formattedJson
        };

        return parsedEntity;
    }
}