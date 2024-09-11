using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedEntity 
    {
        public ParsedEntity(JToken entityJson)
        {
            PersonalOrBusinessCode = BusinessRegisterParser.GetStringValue(entityJson["ariregistri_kood"]);
            BusinessOrLastName = entityJson["nimi"].ToString();
            
            var generalData = entityJson["yldandmed"];
            EntityType = BusinessRegisterParser.GetStringValue(generalData?["oigusliku_vormi_alaliik_tekstina"]);
            EntityTypeAbbreviation = BusinessRegisterParser.GetStringValue(generalData?["oigusliku_vormi_alaliik"]);
            UniqueCode = $"{BusinessRegisterParser.GetStringValue(entityJson["nimi"])}{BusinessRegisterParser.GetStringValue(entityJson["ariregistri_kood"])}";
        }

        public string? PersonalOrBusinessCode { get; set; }
        public string BusinessOrLastName { get; set; }
        public string? EntityTypeAbbreviation { get; set; }
        public string? EntityType { get; set; }
        public string UniqueCode { get; set; }
        public string? FormattedJson { get; set; }
    }
}