using ModelConversionApp.Models;
using System.Text.Json;
using System.Xml;
using System.Xml.Schema;

namespace ModelConversionApp.Reader.Loader;

internal class ErStudioLoader : ILoader
{
    public async Task LoadModelAsync(string filePath)
    {
        try
        {
            // Parse XML file to get XML schema definition
            var doc = new XmlDocument();
            doc.Load(filename: filePath);

            // Create XML namespace manager for resolving namespaces
            var nsMgr = new XmlNamespaceManager(doc.NameTable);
            nsMgr.AddNamespace("dataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine");
            nsMgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

            // Select XML node with schema definition
            var databaseNode = doc.SelectSingleNode(xpath: "//dataSourceView:DataSourceID", nsmgr: nsMgr);
            var schemaNode = doc.SelectSingleNode(xpath: "//dataSourceView:Schema/xs:schema", nsmgr: nsMgr);

            // Create Database and list of tables
            var database = new Database
            {
                Name = databaseNode.InnerXml.Trim(),
            };
            var tables = new List<Table>();
            
            var databaseFileContent = new
            {
                name = database.Name,
                properties = database,
                type = database.EntityType,
            };
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
            };

            Directory.CreateDirectory(path: $"{Directory.GetCurrentDirectory()}/database");
            Directory.CreateDirectory(path: $"{Directory.GetCurrentDirectory()}/database/{database.Name}/table");
            Directory.CreateDirectory(path: $"{Directory.GetCurrentDirectory()}/database/{database.Name}/relationship");
            // var databaseJsonString = JsonSerializer.Serialize(value: databaseFileContent, options: options);
            using FileStream fileStream = File.Create(path: $"./database/{database.Name}.json");
            await JsonSerializer.SerializeAsync(utf8Json: fileStream, value: databaseFileContent, options: options);
            await fileStream.DisposeAsync();


            // Convert to database
            if (schemaNode.HasChildNodes)
            {
                foreach (XmlNode tableNode in schemaNode.ChildNodes)
                {
                    if (tableNode.Attributes.Count > 0 && tableNode.Attributes.GetNamedItem(name: "msprop:TableType").Value == "Table")
                    {
                        var table = CreateTable(tableNode: tableNode);
                    }
                    
                }
            }


            // var xmlStream = Utils.StringToStream(input: schemaNode.InnerXml);

            // Read XML schema
            // var schema = XmlSchema.Read(stream: xmlStream, validationEventHandler: ValidationCallback);

            // var reader = new XmlTextReader()
            // schema = XmlSchema.Read(schemaNode.InnerXml);
            // var reader = new XmlTextReader(filePath);
            // schema = XmlSchema.Read(reader: reader, validationEventHandler: ValidationCallback);
            // schema.Write(Console.Out);
            Console.WriteLine("Hello World");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while loading the Model schema definition: '{ex.Message}'");
        }
    }

    private Table CreateTable(XmlNode tableNode)
    {
        var table = new Table
        {
            Name = tableNode.Attributes.GetNamedItem(name: "msprop:DbTableName").Value,
            Description = "",
            Namespace = new Namespace
            {
                DatabaseName = tableNode.Attributes.GetNamedItem(name: "msprop:DbSchemaName").Value
            }
        };

        if (tableNode.HasChildNodes)
        {
            foreach (XmlNode childNode in tableNode.ChildNodes)
            {
                Console.WriteLine("Hello World");
                if (childNode.Name == "xs:complexType" && childNode.HasChildNodes)
                {
                    foreach (XmlAttribute sequenceNode in childNode.ChildNodes)
                    {
                        var columns = this.CreateColumns(sequenceNode: sequenceNode);
                        table.AddColumns(columns: columns);
                    }
                }
                else if (childNode.Name == "xs:unique")
                {
                    if (childNode.Attributes.Count > 0 && childNode.Attributes.GetNamedItem(name: "name").Value.Contains(value: "_PK"))
                    {
                        // Define primary key
                    }
                    else if (childNode.Attributes.Count > 0 && childNode.Attributes.GetNamedItem(name: "name").Value.Contains(value: "_RELATIONSHIP"))
                    {
                        // Define Relationship
                    }
                }

                if (childNode.Attributes.Count > 0 && childNode.Attributes.GetNamedItem(name: "msprop:TableType").Value == "Table")
                {
                    Console.WriteLine("Hello World");
                }
            }
        }

        return table;
    }

    private List<Column> CreateColumns(XmlNode sequenceNode)
    {
        var columns = new List<Column>();

        foreach (XmlNode columnNode in sequenceNode.ChildNodes)
        {
            if (columnNode.Name == "xs:element" && columnNode.Attributes.Count > 0)
            {
                var column = new Column
                {
                    Name = columnNode.Attributes.GetNamedItem(name: "msprop:DbColumnName").Value,
                    Description = "",
                    OriginDataTypeName = new OriginDataType
                    {
                        TypeName = columnNode.Attributes.GetNamedItem(name: "type").Value,
                        IsComplexType = false,
                        IsNullable = Convert.ToBoolean(columnNode.Attributes.GetNamedItem(name: "nillable").Value),
                        Length = string.IsNullOrEmpty(columnNode.Attributes?.GetNamedItem(name: "msdata:DataSize").Value) ? 0 : Convert.ToInt32(columnNode.Attributes?.GetNamedItem("name").Value),
                        Properties = new OriginDataTypeProperties
                        {
                            HiveTypeString = columnNode.Attributes.GetNamedItem(name: "type").Value,
                        },
                    },
                };
                columns.Add(column);
            }
        }
        return columns;
    }

    private string CreatePrimaryKey(XmlNode uniqueNode)
    {
        var primaryKey = "";

        foreach (XmlNode childNode in uniqueNode.ChildNodes)
        {
            if (childNode.Name == "xs:field" && string.IsNullOrEmpty(childNode.Attributes?.GetNamedItem(name: "msprop:TableType")?.Value))
            {
                primaryKey += childNode.Attributes?.GetNamedItem(name: "msprop:TableType")?.Value
            }
        }
        return primaryKey;
    }

    private static void ValidationCallback(object sender, ValidationEventArgs args)
    {
        if (args.Severity == XmlSeverityType.Warning)
        {
            Console.Write($"WARNING: {args.Message}");
        }
        else if (args.Severity == XmlSeverityType.Error)
        {
            Console.Write($"ERROR: {args.Message}");
        }
    }
}
