using ModelConversionApp.Models.LakeDatabase;
using System.Text.Json;
using System.Xml;
using System.Xml.Schema;

namespace ModelConversionApp.Reader;

internal class ErStudioLoader : ILoader
{
    private string FilePath;

    public ErStudioLoader(string filePath)
    {
        this.FilePath = filePath;
    }

    public (List<Table> tables, List<Relationship> relationships) LoadModel()
    {
        // Load XML file
        var doc = new XmlDocument();
        doc.Load(filename: this.FilePath);

        // Create XML namespace manager for resolving namespaces
        var nsMgr = new XmlNamespaceManager(doc.NameTable);
        nsMgr.AddNamespace("dataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine");
        nsMgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

        // Select XML node with schema definition
        // var databaseNode = doc.SelectSingleNode(xpath: "//dataSourceView:DataSourceID", nsmgr: nsMgr);
        var schemaNode = doc.SelectSingleNode(xpath: "//dataSourceView:Schema/xs:schema", nsmgr: nsMgr);

        // Create output lists
        var tables = new List<Table>();
        var relationships = new List<Relationship>();

        // Convert to lake database
        foreach (XmlNode tableNode in schemaNode.ChildNodes)
        {
            if (tableNode.Name == "xs:element" && GetAttributeValue(node: tableNode, attributeName: "msprop:TableType") == "Table")
            {
                var (table, relationships_new) = CreateTableDefinition(tableNode: tableNode);
                tables.Add(table);
                relationships.AddRange(relationships_new);
            }
            else if (tableNode.Name == "xs:simpleType")
            {
                // TODO: Create dict with Types
            }

        }
        return (tables, relationships);
    }

    private (Table, List<Relationship>) CreateTableDefinition(XmlNode tableNode)
    {
        var table = new Table
        {
            Name = GetAttributeValue(node: tableNode, attributeName: "msprop:DbTableName"),
            Description = "",
            Namespace = new Namespace
            {
                DatabaseName = GetAttributeValue(node: tableNode, attributeName: "msprop:DbSchemaName"),
            },
        };
        var relationships = new List<Relationship>();

        foreach (XmlNode childNode in tableNode.ChildNodes)
        {
            if (childNode.Name == "xs:complexType" && childNode.HasChildNodes)
            {
                foreach (XmlNode sequenceNode in childNode.ChildNodes)
                {
                    var columns = this.CreateColumns(sequenceNode: sequenceNode);
                    table.AddColumns(columns: columns);
                }
            }
            else if (childNode.Name == "xs:unique")
            {
                if (GetAttributeValue(node: childNode, attributeName: "name").Contains(value: "_PK"))
                {
                    var primaryKeys = this.CreatePrimaryKeys(uniqueNode: childNode);
                    table.Properties.PrimaryKeys = primaryKeys;
                }
                else if (GetAttributeValue(node: childNode, attributeName: "name").Contains(value: "_RELATIONSHIP"))
                {
                    var relationships_new = this.CreateRelationships(uniqueNode: childNode, sourceTableName: table.Name, databaseName: table.Namespace.DatabaseName);
                    relationships.AddRange(relationships_new);
                }
            }
        }
        return (table, relationships);
    }

    private List<Relationship> CreateRelationships(XmlNode uniqueNode, string sourceTableName, string databaseName)
    {
        var relationships = new List<Relationship>();
        int i = 1;

        // Parse name of the other table
        var sinkTableName = GetAttributeValue(node: uniqueNode, attributeName: "name").Split(separator: "_RELATIONSHIP")[0];

        foreach (XmlNode childNode in uniqueNode)
        {
            if (childNode.Name == "xs:field")
            {
                var relationship = new Relationship
                {
                    Name = $"{sourceTableName}-{sinkTableName}-{i++}",
                    Namespace = new Namespace
                    {
                        DatabaseName = databaseName,
                    },
                    FromTableName = sourceTableName,
                    ToTableName = sinkTableName,
                    ColumnRelationshipInformations = new ColumnRelationshipInformations
                    {
                        FromColumnName = GetAttributeValue(node: childNode, attributeName: "xpath"),
                        ToColumnName = GetAttributeValue(node: childNode, attributeName: "xpath"),
                    },
                };
                relationships.Add(relationship);
            }
        }
        return relationships;
    }

    private string CreatePrimaryKeys(XmlNode uniqueNode)
    {
        var primaryKey = "";

        foreach (XmlNode childNode in uniqueNode.ChildNodes)
        {
            if (childNode.Name == "xs:field" && !string.IsNullOrEmpty(GetAttributeValue(node: childNode, attributeName: "xpath")))
            {
                primaryKey += childNode.Attributes?.GetNamedItem(name: "xpath")?.Value + ",";
            }
        }
        return primaryKey.Remove(primaryKey.Length - 1);
    }

    private List<Column> CreateColumns(XmlNode sequenceNode)
    {
        var columns = new List<Column>();

        foreach (XmlNode columnNode in sequenceNode.ChildNodes)
        {
            if (columnNode.Name == "xs:element" && columnNode.Attributes?.Count > 0)
            {
                var column = new Column
                {
                    Name = GetAttributeValue(node: columnNode, attributeName: "msprop:DbColumnName"),
                    Description = "",
                    OriginDataTypeName = new OriginDataType
                    {
                        TypeName = GetAttributeValue(node: columnNode, attributeName: "type"),
                        IsComplexType = false,
                        IsNullable = GetAttributeValue(node: columnNode, attributeName: "nillable"), // TODO: Convert.ToBoolean(columnNode.Attributes.GetNamedItem(name: "nillable").Value)
                        Length = GetAttributeValue(node: columnNode, attributeName: "msdata:DataSize"),  // TODO: string.IsNullOrEmpty(columnNode.Attributes?.GetNamedItem(name: "msdata:DataSize")?.Value) ? 0 : Convert.ToInt32(columnNode.Attributes?.GetNamedItem("msdata:DataSize")?.Value),
                        Properties = new OriginDataTypeProperties
                        {
                            HiveTypeString = GetAttributeValue(node: columnNode, attributeName: "type"),
                        },
                    },
                };
                columns.Add(column);
            }
        }
        return columns;
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

    private static string GetAttributeValue(XmlNode node, string attributeName)
    {
        try
        {
            string? attribteValue = node.Attributes?.GetNamedItem(attributeName)?.Value;
            return string.IsNullOrEmpty(attribteValue) ? "" : attribteValue;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Node does not contain attribute with name '{attributeName}'. Exception: '{ex.Message}'");
            return "";
        }
    }
}
