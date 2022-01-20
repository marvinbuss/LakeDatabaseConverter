using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class Column
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";
    [JsonPropertyName("OriginDataTypeName")]
    public OriginDataType OriginDataTypeName { get; set; }
}
