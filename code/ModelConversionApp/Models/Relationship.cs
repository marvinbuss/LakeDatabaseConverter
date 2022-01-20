using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class Relationship
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("EntityType")]
    public string EntityType { get; set; } = "RELATIONSHIP";
    [JsonPropertyName("Origin")]
    public Origin Origin { get; set; } = new Origin();
    [JsonPropertyName("Namespace")]
    public Namespace Namespace { get; set; }
    [JsonPropertyName("FromTableName")]
    public string FromTableName { get; set; }
    [JsonPropertyName("ToTableName")]
    public string ToTableName { get; set; }
    [JsonPropertyName("RelationshipType")]
    public int RelationshipType { get; set; } = 0;
}
