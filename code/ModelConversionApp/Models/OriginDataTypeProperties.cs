using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class OriginDataTypeProperties
{
    [JsonPropertyName("HIVE_TYPE_STRING")]
    public string HiveTypeString { get; set; }
    [JsonPropertyName("TimestampFormat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TimestampFormat { get; set; } = null;  // for timestamp use "YYYY-MM-DD HH:MM:SS.fffffffff"
}
