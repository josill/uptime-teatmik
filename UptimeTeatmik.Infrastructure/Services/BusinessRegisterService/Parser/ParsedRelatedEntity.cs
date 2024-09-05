using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedRelatedEntity : IEquatable<ParsedRelatedEntity>
    {
        public ParsedRelatedEntity(JToken relatedEntityJson)
        {
            PersonalOrBusinessCode = relatedEntityJson["isikukood_registrikood"].ToString();
            FirstName = relatedEntityJson["eesnimi"].ToString();
            LastOrBusinessName = relatedEntityJson["nimi_arinimi"].ToString();
            EntityType = relatedEntityJson["isiku_roll_tekstina"].ToString();
            EntityTypeAbbreviation = relatedEntityJson["isiku_roll"].ToString();
        }

        public string PersonalOrBusinessCode { get; }
        public string? FirstName { get; }
        public string LastOrBusinessName { get; }
        public string EntityType { get; }
        public string EntityTypeAbbreviation { get; }
        public string? FormattedJson { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ParsedRelatedEntity);
        }

        public bool Equals(ParsedRelatedEntity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return PersonalOrBusinessCode == other.PersonalOrBusinessCode &&
                   FirstName == other.FirstName &&
                   LastOrBusinessName == other.LastOrBusinessName &&
                   EntityType == other.EntityType &&
                   EntityTypeAbbreviation == other.EntityTypeAbbreviation;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(PersonalOrBusinessCode, FirstName, LastOrBusinessName, EntityType, EntityTypeAbbreviation);
        }
    }
}