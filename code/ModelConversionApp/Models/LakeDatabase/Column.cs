using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class Column
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";
    [JsonPropertyName("OriginDataTypeName")]
    public OriginDataType? OriginDataTypeName { get; set; }
}
