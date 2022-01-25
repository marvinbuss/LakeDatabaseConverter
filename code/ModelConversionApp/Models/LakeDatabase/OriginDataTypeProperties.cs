using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class OriginDataTypeProperties
{
    [JsonPropertyName("HIVE_TYPE_STRING")]
    public string HiveTypeString { get; set; } = string.Empty;
    [JsonPropertyName("TimestampFormat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TimestampFormat { get; set; } = null;  // for timestamp use "YYYY-MM-DD HH:MM:SS.fffffffff"
}
