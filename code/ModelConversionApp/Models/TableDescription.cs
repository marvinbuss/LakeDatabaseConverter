namespace ModelConversionApp.Models;

internal class TableDescription
{
    private readonly List<Column> Columns;
    private readonly Format Format;
    private readonly Source? Source;
    private readonly TableDescriptionProperties Properties;
    private readonly bool Compressed;
    private readonly bool IsStoredAsSubdirectories;

    public TableDescription()
    {
        this.Columns = new List<Column>();
        this.Format = new Format(formatType: "parquet");
        this.Source = null;
        this.Properties = new TableDescriptionProperties();
        this.Compressed = false;
        this.IsStoredAsSubdirectories = false;
    }

    internal void AddColumn(Column column)
    {
        this.Columns.Add(column);
    }
}
