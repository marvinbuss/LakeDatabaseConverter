using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

public class SourceProperties
{
    [JsonPropertyName("LinkedServiceName")]
    public string LinkedServiceName { get; set; } = string.Empty;
    [JsonPropertyName("FormatType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FormatType { get; set; } = null;
    [JsonPropertyName("LocationSetToDatabaseDefault")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool LocationSetToDatabaseDefault { get; set; } = false;
}
