namespace ModelConversionApp.Models;

internal class Table
{
    private readonly string Name;
    private readonly string Description;
    private readonly string EntityType;
    private readonly string TableType;
    private readonly int Retention;
    private readonly bool Temporary;
    private readonly bool IsRewriteEnabled;
    private readonly TableNamespace Namespace;
    private readonly TableProperties Properties;
    private readonly Origin Origin;
    private readonly TableDescription StorageDescriptor; 

    public Table(string name, string description, string databaseName, string primaryKeys)
    {
        this.Name = name;
        this.Description = description;
        this.EntityType = "TABLE";
        this.TableType = "EXTERNAL";
        this.Retention = 0;
        this.Temporary = false;
        this.IsRewriteEnabled = false;
        this.Namespace = new TableNamespace(databaseName: databaseName);
        this.Properties = new TableProperties(description: description, displayFolderInfo: "{\"name\":\"Others\",\"colorCode\":\"\"}", primaryKeys: primaryKeys);
        this.Origin = new Origin(type: "SPARK");
        this.StorageDescriptor = new TableDescription();
    }

    internal void AddColumn(Column column)
    {
        this.StorageDescriptor.AddColumn(column);
    }
}
