using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedRelatedEntity
    {
        public ParsedRelatedEntity(JToken relatedEntityJson)
        {
            BusinessOrPersonalCode = BusinessRegisterParser.GetStringValue(relatedEntityJson["isikukood_registrikood"]);
            FirstName = BusinessRegisterParser.GetStringValue(relatedEntityJson["eesnimi"]);
            BusinessOrLastName = relatedEntityJson["nimi_arinimi"].ToString();
            EntityType = BusinessRegisterParser.GetStringValue(relatedEntityJson["isiku_roll_tekstina"]);
            EntityTypeAbbreviation = BusinessRegisterParser.GetStringValue(relatedEntityJson["isiku_roll"]);
            UniqueCode = $"{BusinessRegisterParser.GetStringValue(relatedEntityJson["eesnimi"])}{BusinessRegisterParser.GetStringValue(relatedEntityJson["nimi_arinimi"])}{BusinessRegisterParser.GetStringValue(relatedEntityJson["isikukood_registrikood"])}";
        }

        public string? BusinessOrPersonalCode { get; }
        public string? FirstName { get; }
        public string BusinessOrLastName { get; }
        public string? EntityType { get; }
        public string? EntityTypeAbbreviation { get; }
        public string? UniqueCode { get; }
        public string? FormattedJson { get; set; }
    }
}