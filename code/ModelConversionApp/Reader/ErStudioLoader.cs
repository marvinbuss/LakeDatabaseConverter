using ModelConversionApp.Models.LakeDatabase;
using ModelConversionApp.Models.Reader;
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
        var typeMapping = new Dictionary<string, DataType>();

        // Convert to lake database
        if (schemaNode != null)
        {
            foreach (XmlNode childNode in schemaNode.ChildNodes)
            {
                if (childNode.Name == "xs:element" && GetAttributeValue(node: childNode, attributeName: "msprop:TableType") == "Table")
                {
                    var (table, relationships_new) = CreateTableDefinition(tableNode: childNode);
                    tables.Add(table);
                    relationships.AddRange(relationships_new);
                }
                else if (childNode.Name == "xs:simpleType")
                {
                    var originalType = GetAttributeValue(node: childNode, attributeName: "name");
                    var lakeDatabaseType = CreateTypeDefinition(dataTypeNode: childNode);
                    if (lakeDatabaseType != null)
                    {
                        typeMapping.Add(key: originalType, value: lakeDatabaseType);
                    }
                }
            }
        }

        // Map types in tables
        foreach (var table in tables)
        {
            foreach (var column in table.StorageDescriptor.Columns)
            {
                DataType newDataType;
                var keyAvailable = typeMapping.TryGetValue(key: column.OriginDataTypeName.TypeName, value: out newDataType);

                if (keyAvailable)
                {
                    column.OriginDataTypeName.TypeName = newDataType.TypeName;
                    column.OriginDataTypeName.Properties.HiveTypeString = newDataType.TypeName;
                    column.OriginDataTypeName.Properties.DateFormat = newDataType.Restrictions.DateFormat;
                    column.OriginDataTypeName.Properties.TimestampFormat = newDataType.Restrictions.TimestampFormat;
                    column.OriginDataTypeName.Length = newDataType.Restrictions.Length;
                    column.OriginDataTypeName.Precision = newDataType.Restrictions.Precision;
                    column.OriginDataTypeName.Scale = newDataType.Restrictions.Scale;
                }
            }
        }

        return (tables, relationships);
    }

    private DataType? CreateTypeDefinition(XmlNode dataTypeNode)
    {
        DataType? dataType = null;
        foreach (XmlNode childNode in dataTypeNode.ChildNodes)
        {
            if (childNode.Name == "xs:restriction")
            {
                dataType = new DataType
                {
                    TypeName = DataTypeConverter.ConvertErStudioToLakeDatabaseDataType(dataType: GetAttributeValue(node: childNode, attributeName: "base")),
                };

                foreach (XmlNode restrictionChildNode in childNode.ChildNodes)
                {
                    if (restrictionChildNode.Name == "xs:maxLength")
                    {
                        dataType.Restrictions.Length = Convert.ToInt32(GetAttributeValue(node: restrictionChildNode, attributeName: "value"));
                    }
                    else if (restrictionChildNode.Name == "xs:totalDigits")
                    {
                        dataType.Restrictions.Precision = Convert.ToInt32(GetAttributeValue(node: restrictionChildNode, attributeName: "value"));
                        dataType.Restrictions.Scale = 2;
                    }
                }

                if (dataType.TypeName == "date")
                {
                    dataType.Restrictions.DateFormat = "YYYY-MM-DD";
                }
                else if (dataType.TypeName == "timestamp")
                {
                    dataType.Restrictions.TimestampFormat = "YYYY-MM-DD HH:MM:SS.fffffffff";
                }
            }
        }
        return dataType;
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
                    var relationship = this.CreateRelationships(uniqueNode: childNode, sourceTableName: table.Name, databaseName: table.Namespace.DatabaseName);
                    relationships.Add(relationship);
                }
            }
        }
        return (table, relationships);
    }

    private Relationship CreateRelationships(XmlNode uniqueNode, string sourceTableName, string databaseName)
    {
        // Parse name of the other table
        var sinkTableName = GetAttributeValue(node: uniqueNode, attributeName: "name").Split(separator: "_RELATIONSHIP")[0];

        var relationship = new Relationship
        {
            Name = $"{sourceTableName}-{sinkTableName}",
            Namespace = new Namespace
            {
                DatabaseName = databaseName,
            },
            FromTableName = sourceTableName,
            ToTableName = sinkTableName
        };

        foreach (XmlNode childNode in uniqueNode)
        {
            if (childNode.Name == "xs:field")
            {
                var columnRelationshipInformation = new ColumnRelationshipInformation
                {
                    FromColumnName = GetAttributeValue(node: childNode, attributeName: "xpath"),
                    ToColumnName = GetAttributeValue(node: childNode, attributeName: "xpath"),
                };
                relationship.AddColumnRelationship(columnRelationshipInformation: columnRelationshipInformation);
            }
        }
        return relationship;
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
                        IsNullable = Convert.ToBoolean(GetAttributeValue(node: columnNode, attributeName: "nillable")),
                        Length = Convert.ToInt32(GetAttributeValue(node: columnNode, attributeName: "msdata:DataSize")),
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
