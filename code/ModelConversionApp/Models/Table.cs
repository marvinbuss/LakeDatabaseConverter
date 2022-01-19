namespace ModelConversionApp.Models;

internal class Table
{
    private readonly string Name;
    private readonly string Description;
    private readonly string Version;
    private readonly string EntityType;
    private readonly string TableType;
    private readonly int Retention;
    private readonly bool Temporary;
    private readonly bool IsRewriteEnabled;
    private readonly Namespace Namespace;
    private readonly TableProperties Properties;
    private readonly Origin Origin;

    public Table(string name, string description, string version, string databaseName, string primaryKeys)
    {
        this.Name = name;
        this.Description = description;
        this.Version = version;
        this.EntityType = "TABLE";
        this.TableType = "EXTERNAL";
        this.Retention = 0;
        this.Temporary = false;
        this.IsRewriteEnabled = false;
        this.Namespace = new Namespace(databaseName: databaseName);
        this.Properties = new TableProperties(description: description, displayFolderInfo: "{\"name\":\"Others\",\"colorCode\":\"\"}", primaryKeys: primaryKeys);
        this.Origin = new Origin(type: "SPARK");
    }
}
