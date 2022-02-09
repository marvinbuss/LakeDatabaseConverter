using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class Relationship
{
    [JsonPropertyName("Namespace")]
    public Namespace Namespace { get; set; } = new Namespace();
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("EntityType")]
    public string EntityType { get; set; } = "RELATIONSHIP";
    [JsonPropertyName("Origin")]
    public Origin Origin { get; set; } = new Origin();
    [JsonPropertyName("FromTableName")]
    public string FromTableName { get; set; } = string.Empty;
    [JsonPropertyName("ToTableName")]
    public string ToTableName { get; set; } = string.Empty;
    [JsonPropertyName("ColumnRelationshipInformations")]
    public List<ColumnRelationshipInformation> ColumnRelationshipInformations { get; set; } = new List<ColumnRelationshipInformation>();
    [JsonPropertyName("RelationshipType")]
    public int RelationshipType { get; set; } = 0;

    internal void AddColumnRelationship(ColumnRelationshipInformation columnRelationshipInformation)
    {
        this.ColumnRelationshipInformations.Add(columnRelationshipInformation);
    }
}
