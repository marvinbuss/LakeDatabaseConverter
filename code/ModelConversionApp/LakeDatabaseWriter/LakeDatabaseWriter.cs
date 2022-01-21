using ModelConversionApp.Models;
using System.Text.Json;

namespace ModelConversionApp.Writer;

internal class LakeDatabaseWriter
{
    private List<Table> Tables;
    private List<Relationship> Relationships;

    /// <summary>
    /// Creates an instance of writer
    /// </summary>
    /// <param name="tables">List of table definitions.</param>
    /// <param name="relationships">List of relationship definitions.</param>
    public LakeDatabaseWriter(List<Table> tables, List<Relationship> relationships)
    {
        this.Tables = tables;
        this.Relationships = relationships;
    }

    /// <summary>
    /// Writes databases, tables and relationships in teh Lake Database format and structure to storage.
    /// </summary>
    public void WriteLakeDatabase()
    {
        // Create list of databases
        var databaseNames = new List<string>();

        // Define Json serielizer options
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        // Write lake database tables to folder
        foreach (var table in Tables)
        {
            databaseNames.Add(item: table.Namespace.DatabaseName);
            var tableFileContent = new
            {
                name = table.Name,
                properties = table,
                type = table.EntityType,
            };
            string fileName = $"{Directory.GetCurrentDirectory()}/database/{table.Namespace.DatabaseName}/table/{table.Name}.json";
            string jsonString = JsonSerializer.Serialize(value: tableFileContent, options: options);
            File.WriteAllText(fileName, jsonString);
        }

        // Write lake database relationships to folder
        foreach (var relationship in Relationships)
        {
            var relationshipFileContent = new
            {
                name = relationship.Name,
                properties = relationship,
                type = relationship.EntityType,
            };
            string fileName = $"{Directory.GetCurrentDirectory()}/database/{relationship.Namespace.DatabaseName}/relationship/{relationship.Name}.json";
            string jsonString = JsonSerializer.Serialize(value: relationshipFileContent, options: options);
            File.WriteAllText(fileName, jsonString);
        }

        // Write  lake database databases to folder
        foreach (var databaseName in databaseNames.Distinct<string>())
        {
            var database = new Database
            {
                Name = databaseName,
            };
            var databasefilecontent = new
            {
                name = database.Name,
                properties = database
                type = database.EntityType,
            };

            string fileName = $"{Directory.GetCurrentDirectory()}/database/{database.Name}.json";
            string jsonString = JsonSerializer.Serialize(value: databasefilecontent, options: options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
