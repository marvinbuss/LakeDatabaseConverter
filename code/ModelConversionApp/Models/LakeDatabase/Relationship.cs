using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class Relationship
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("EntityType")]
    public string EntityType { get; set; } = "RELATIONSHIP";
    [JsonPropertyName("Origin")]
    public Origin Origin { get; set; } = new Origin();
    [JsonPropertyName("Namespace")]
    public Namespace Namespace { get; set; } = new Namespace();
    [JsonPropertyName("FromTableName")]
    public string FromTableName { get; set; } = string.Empty;
    [JsonPropertyName("ToTableName")]
    public string ToTableName { get; set; } = string.Empty;
    [JsonPropertyName("RelationshipType")]
    public int RelationshipType { get; set; } = 0;
    [JsonPropertyName("ColumnRelationshipInformations")]
    public ColumnRelationshipInformations? ColumnRelationshipInformations { get; set; }
}
