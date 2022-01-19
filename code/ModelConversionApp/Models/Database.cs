namespace ModelConversionApp.Models;

internal class Database
{
    private readonly string Name;
    private readonly string Description;
    private readonly string EntityType;
    private readonly Origin Origin;
    private readonly DatabaseProperties Properties;
    private readonly Source? Source;
    private readonly List<Table> Tables;

    public Database(string name, string description)
    {
        this.Name = name;
        this.Description = description;
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
