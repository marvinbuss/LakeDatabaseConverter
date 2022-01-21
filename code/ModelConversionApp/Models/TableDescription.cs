using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class TableDescription
{
    [JsonPropertyName("Columns")]
    public List<Column> Columns { get; set; } = new List<Column>();
    [JsonPropertyName("Format")]
    public Format Format { get; set; } = new Format();
    [JsonPropertyName("Source")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Source? Source = null;
    [JsonPropertyName("Properties")]
    public TableDescriptionProperties Properties { get; set; } = new TableDescriptionProperties();
    [JsonPropertyName("Compressed")]
    public bool Compressed { get; set; } = false;
    [JsonPropertyName("IsStoredAsSubdirectories")]
    public bool IsStoredAsSubdirectories { get; set; } = false;

    internal void AddColumn(Column column)
    {
        this.Columns.Add(column);
    }

    internal void AddColumns(List<Column> columns)
    {
        this.Columns.AddRange(columns);
    }
}
