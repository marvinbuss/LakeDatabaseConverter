using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class Namespace
{
    [JsonPropertyName("DatabaseName")]
    public string DatabaseName { get; set; } = string.Empty;
}
