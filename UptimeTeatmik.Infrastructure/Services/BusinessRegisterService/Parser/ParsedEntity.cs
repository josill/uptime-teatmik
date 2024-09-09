using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedEntity
    {
        public ParsedEntity(JToken entityJson)
        {
            PersonalOrBusinessCode = entityJson["ariregistri_kood"].ToString();
            LastOrBusinessName = entityJson["nimi"].ToString();

            var entityTypeIsEmptyObject = !entityJson["yldandmed"]["oigusliku_vormi_alaliik_tekstina"].HasValues;
            var entityTypeAbbreviationIsEmptyObject = !entityJson["yldandmed"]["oigusliku_vormi_alaliik"].HasValues;
            
            \EntityTypeAbbreviation = !entityTypeAbbreviationIsEmptyObject ? entityJson["yldandmed"]["oigusliku_vormi_alaliik"].ToString() : null;
            EntityType = !entityTypeIsEmptyObject ? entityJson["yldandmed"]["oigusliku_vormi_alaliik_tekstina"]?.ToString() : null;
        }

        public string PersonalOrBusinessCode { get; set; }
        public string LastOrBusinessName { get; set; }
        public string? EntityTypeAbbreviation { get; set; }
        public string? EntityType { get; set; }
        public string? FormattedJson { get; set; }
    }
}