using Newtonsoft.Json.Linq;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService.Parser
{
    public class ParsedRelatedEntity : IEquatable<ParsedRelatedEntity>
    {
        public ParsedRelatedEntity(JToken relatedEntityJson)
        {
            BusinessOrPersonalCode = relatedEntityJson["isikukood_registrikood"].ToString();
            FirstName = relatedEntityJson["eesnimi"].ToString();
            BusinessOrLastName = relatedEntityJson["nimi_arinimi"].ToString();
            EntityType = relatedEntityJson["isiku_roll_tekstina"].ToString();
            EntityTypeAbbreviation = relatedEntityJson["isiku_roll"].ToString();
            UniqueCode = $"{FirstName}{BusinessOrLastName}{BusinessOrPersonalCode}";
        }

        public string BusinessOrPersonalCode { get; }
        public string? FirstName { get; }
        public string BusinessOrLastName { get; }
        public string EntityType { get; }
        public string EntityTypeAbbreviation { get; }
        public string UniqueCode { get; set; }
        public string? FormattedJson { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ParsedRelatedEntity);
        }

        public bool Equals(ParsedRelatedEntity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return BusinessOrPersonalCode == other.BusinessOrPersonalCode &&
                   FirstName == other.FirstName &&
                   BusinessOrLastName == other.BusinessOrLastName &&
                   EntityType == other.EntityType &&
                   EntityTypeAbbreviation == other.EntityTypeAbbreviation;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(BusinessOrPersonalCode, FirstName, BusinessOrLastName, EntityType, EntityTypeAbbreviation);
        }
    }
}