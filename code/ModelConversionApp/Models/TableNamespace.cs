using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class TableNamespace
{
    [JsonPropertyName("DatabaseName")]
    public string DatabaseName { get; set; }
}
