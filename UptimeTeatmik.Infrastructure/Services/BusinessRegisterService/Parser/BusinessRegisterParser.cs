using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser;

public static class BusinessRegisterParser
{
    public static JToken ParseBusinessRelatedEntities(string responseContent)
    {
        var jObject = JObject.Parse(responseContent);
        var relatedEntities = jObject["keha"]?["ettevotjad"]?["item"]?[0]?["isikuandmed"]?["kaardile_kantud_isikud"]?["item"];
        if (relatedEntities == null) throw new InvalidOperationException("Unable to retrieve information about related entity.");

        return relatedEntities;
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
    
    public static string? GetStringValue(JToken? token)
    {
        if (token == null || token.Type == JTokenType.Null)
            return null;

        Console.WriteLine($"Token: {token}, Type: {token.Type}");

        if (token.Type == JTokenType.Object && !token.HasValues)
            return null;  // Return null for empty objects

        return token.ToString();
    }
}