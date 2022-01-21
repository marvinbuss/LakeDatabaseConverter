using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

public class SourceProperties
{
    [JsonPropertyName("LinkedServiceName")]
    public string LinkedServiceName { get; set; }
    [JsonPropertyName("FormatType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FormatType { get; set; } = null;
    [JsonPropertyName("LocationSetToDatabaseDefault")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool LocationSetToDatabaseDefault { get; set; } = false;
}
