using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class Namespace
{
    [JsonPropertyName("DatabaseName")]
    public string DatabaseName { get; set; }
}
