using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class Origin
{
    [JsonPropertyName("Type")]
    public string Type { get; set; } = "SPARK";
}
