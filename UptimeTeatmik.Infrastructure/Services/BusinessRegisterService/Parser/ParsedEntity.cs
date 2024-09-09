using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedEntity 
    {
        public ParsedEntity(JToken entityJson)
        {
            PersonalOrBusinessCode = BusinessRegisterParser.GetStringValue(entityJson["ariregistri_kood"]);
            LastOrBusinessName = entityJson["nimi"]?.ToString();
            
            var generalData = entityJson["yldandmed"];
            EntityType = BusinessRegisterParser.GetStringValue(generalData?["oigusliku_vormi_alaliik_tekstina"]);
            EntityTypeAbbreviation = BusinessRegisterParser.GetStringValue(generalData?["oigusliku_vormi_alaliik"]);
        }

        public string? PersonalOrBusinessCode { get; set; }
        public string? LastOrBusinessName { get; set; }
        public string? EntityTypeAbbreviation { get; set; }
        public string? EntityType { get; set; }
        public string? FormattedJson { get; set; }
    }
}