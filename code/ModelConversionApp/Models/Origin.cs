using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class Origin
{
    [JsonPropertyName("Type")]
    public string Type { get; set; } = "SPARK";
}
