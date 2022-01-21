using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class Source
{
    [JsonPropertyName("Provider")]
    public string Provider { get; set; } = "ADLS";
    [JsonPropertyName("Location")]
    public string Location { get; set; }
    [JsonPropertyName("Properties")]
    public SourceProperties Properties { get; set; } = new SourceProperties();
}
