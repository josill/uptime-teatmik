using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedEntity
    {
        public ParsedEntity(JToken entityJson)
        {
            PersonalOrBusinessCode = entityJson["ariregistri_kood"].ToString();
            LastOrBusinessName = entityJson["nimi"].ToString();
            EntityTypeAbbreviation = entityJson["yldandmed"]["oigusliku_vormi_alaliik"].ToString();
            EntityType = entityJson["yldandmed"]["oigusliku_vormi_alaliik_tekstina"].ToString();
        }

        public string PersonalOrBusinessCode { get; set; }
        public string LastOrBusinessName { get; set; }
        public string EntityTypeAbbreviation { get; set; }
        public string EntityType { get; set; }
        public string? FormattedJson { get; set; }
    }
}