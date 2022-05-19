namespace ModelConversionApp.Models.LakeDatabase;

internal class LakeDatabase
{
    public Database Database { get; set; } = new Database();
    public List<Table> Tables { get; set; } = new List<Table>();
    public List<Relationship> Relationships { get; set; } = new List<Relationship>();
}
