using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class OriginDataTypeProperties
{
    [JsonPropertyName("HIVE_TYPE_STRING")]
    public string HiveTypeString { get; set; } = string.Empty;
    [JsonPropertyName("TimestampFormat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TimestampFormat { get; set; } = null;  // for timestamp use "YYYY-MM-DD HH:MM:SS.fffffffff"
    [JsonPropertyName("DateFormat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DateFormat { get; set; } = null;  // for date use "YYYY-MM-DD"
}
