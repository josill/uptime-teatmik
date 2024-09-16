using System.Text.Json.Serialization;

namespace UptimeTeatmik.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventType
{
    [JsonPropertyName("Created")]
    Created,
    [JsonPropertyName("Updated")]
    Updated,
    [JsonPropertyName("UpdateFailed")]
    UpdateFailed,
    [JsonPropertyName("Deleted")]
    Deleted
}