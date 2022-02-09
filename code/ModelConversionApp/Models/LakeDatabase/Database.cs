using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class Database
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";
    [JsonPropertyName("EntityType")]
    public string EntityType { get; set; } = "DATABASE";
    [JsonPropertyName("Origin")]
    public Origin Origin { get; set; } = new Origin();
    [JsonPropertyName("Properties")]
    public DatabaseProperties Properties { get; set; } = new DatabaseProperties();
    [JsonPropertyName("Source")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Source? Source { get; set; } = null;
}
