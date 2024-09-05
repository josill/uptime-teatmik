using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedPerson
    {
        public ParsedPerson(JToken personJson)
        {
            PersonalOrBusinessCode = personJson["isikukood_registrikood"].ToString();
            FirstName = personJson["eesnimi"].ToString();
            LastOrBusinessName = personJson["nimi_arinimi"].ToString();
            FormattedJson = JsonConvert.SerializeObject(
                JsonConvert.DeserializeObject(personJson.ToString()),
                Formatting.Indented);

            PersonTypeAbbreviation = personJson["isiku_roll"].ToString();
            PersonType = personJson["isiku_roll_tekstina"].ToString();
        }

        public string PersonalOrBusinessCode { get; set; }
        public string FirstName { get; set; }
        public string LastOrBusinessName { get; set; }
        public string PersonType { get; set; }
        public string PersonTypeAbbreviation { get; set; }
        public string FormattedJson { get; set; }
    }
}