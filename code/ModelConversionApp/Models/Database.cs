namespace ModelConversionApp.Models;

internal class Database
{
    private readonly string Name;
    private readonly string Description;
    private readonly string Version;
    private readonly string EntityType;
    private readonly Origin Origin;
    private readonly DatabaseProperties Properties;
    private readonly string? Source;
    private readonly List<Table> Tables;

    public Database(string name, string description, string version)
    {
        this.Name = name;
        this.Description = description;
        this.Version = version;
        this.EntityType = "DATABASE";
        this.Origin = new Origin(type: "SPARK");
        this.Properties = new DatabaseProperties(isSyMSCDMDatabase: true);
        this.Source = null;
        this.Tables = new List<Table>();
    }

    public void AddTable(Table table)
    {
        Tables.Add(table);
    }
}
