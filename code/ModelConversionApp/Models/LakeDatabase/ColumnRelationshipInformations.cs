using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class ColumnRelationshipInformation
{
    [JsonPropertyName("FromColumnName")]
    public string FromColumnName { get; set; } = string.Empty;
    [JsonPropertyName("ToColumnName")]
    public string ToColumnName { get; set; } = string.Empty;
}
