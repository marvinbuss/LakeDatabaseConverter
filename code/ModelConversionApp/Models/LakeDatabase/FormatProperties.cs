using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class FormatProperties
{
    [JsonPropertyName("path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Path { get; set; }
    [JsonPropertyName("FormatTypeSetToDatabaseDefault")]
    public bool FormatTypeSetToDatabaseDefault { get; set; } = false;
}
