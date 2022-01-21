using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class ColumnRelationshipInformations
{
    [JsonPropertyName("FromColumnName")]
    public string FromColumnName { get; set; }
    [JsonPropertyName("ToColumnName")]
    public string ToColumnName { get; set; }
}
