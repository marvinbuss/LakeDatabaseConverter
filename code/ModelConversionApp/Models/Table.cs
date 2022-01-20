using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class Table
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";
    [JsonPropertyName("EntityType")]
    public string EntityType { get; set; } = "TABLE";
    [JsonPropertyName("TableType")]
    public string TableType { get; set; } = "EXTERNAL";
    [JsonPropertyName("Retention")]
    public int Retention { get; set; } = 0;
    [JsonPropertyName("Temporary")]
    public bool Temporary { get; set; } = false;
    [JsonPropertyName("IsRewriteEnabled")]
    public bool IsRewriteEnabled { get; set; } = false;
    [JsonPropertyName("Namespace")]
    public TableNamespace Namespace { get; set; }
    [JsonPropertyName("Properties")]
    public TableProperties Properties { get; set; } = new TableProperties();
    [JsonPropertyName("Origin")]
    public Origin Origin { get; set; } = new Origin();
    [JsonPropertyName("StorageDescriptor")]
    public TableDescription StorageDescriptor { get; set; } = new TableDescription();

    internal void AddColumn(Column column)
    {
        this.StorageDescriptor.AddColumn(column);
    }
}
