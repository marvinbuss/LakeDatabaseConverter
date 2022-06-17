using System.Text.Json.Serialization;

namespace ModelConversionApp.Clients.ErStudio.Models;

public class TableMetadata
{
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}

public class TableSecurityproperty
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("dictionary")]
    public string Dictionary { get; set; }
    [JsonPropertyName("datatype")]
    public string Datatype { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class Table
{
    [JsonPropertyName("schema")]
    public string Schema { get; set; }
    [JsonPropertyName("diagramLink")]
    public string DiagramLink { get; set; }
    [JsonPropertyName("notes")]
    public string Notes { get; set; }
    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; set; }
    [JsonPropertyName("diagramId")]
    public int DiagramId { get; set; }
    [JsonPropertyName("modelId")]
    public int ModelId { get; set; }
    [JsonPropertyName("columns")]
    public List<TableColumn> Columns { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("logicalOnly")]
    public string LogicalOnly { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("securityproperties")]
    public List<TableSecurityproperty> TableSecurityproperties { get; set; }
    [JsonPropertyName("createdAt")]
    public long CreatedAt { get; set; }
    [JsonPropertyName("entityName")]
    public string EntityName { get; set; }
    [JsonPropertyName("isCurrentUserFollowing")]
    public bool IsCurrentUserFollowing { get; set; }
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("diagramURL")]
    public string DiagramURL { get; set; }
    [JsonPropertyName("modelURL")]
    public string ModelURL { get; set; }
    [JsonPropertyName("modelLink")]
    public string ModelLink { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("diagram")]
    public string Diagram { get; set; }
    [JsonPropertyName("alerts")]
    public List<object> Alerts { get; set; }
    [JsonPropertyName("physicalOnly")]
    public string PhysicalOnly { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("submodels")]
    public List<TableSubmodel> TableSubmodels { get; set; }
}

public class TableColumn
{
    [JsonPropertyName("referenceValues")]
    public List<object> ReferenceValues { get; set; }
    [JsonPropertyName("notes")]
    public string Notes { get; set; }
    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("length")]
    public string Length { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("scale")]
    public string Scale { get; set; }
    [JsonPropertyName("keytype")]
    public string Keytype { get; set; }
    [JsonPropertyName("logicalOnly")]
    public string LogicalOnly { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("securityproperties")]
    public List<object> TableColumnSecurityproperties { get; set; }
    [JsonPropertyName("alerts")]
    public List<object> Alerts { get; set; }
    [JsonPropertyName("createdAt")]
    public long CreatedAt { get; set; }
    [JsonPropertyName("_default")]
    public string Default { get; set; }
    [JsonPropertyName("physicalOnly")]
    public string PhysicalOnly { get; set; }
    [JsonPropertyName("datatype")]
    public string Datatype { get; set; }
    [JsonPropertyName("isNullable")]
    public string IsNullable { get; set; }
    [JsonPropertyName("domain")]
    public string Domain { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("attributeName")]
    public string AttributeName { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("foreignKey")]
    public string ForeignKey { get; set; }
    [JsonPropertyName("PrimaryKey")]
    public string PrimaryKey { get; set; }
}

public class TableSubmodel
{
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("parentSubmodelId")]
    public string ParentSubmodelId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class TableResponse
{
    [JsonPropertyName("metadata_")]
    public TableMetadata TableMetadata { get; set; }
    [JsonPropertyName("tables")]
    public List<Table> Tables { get; set; }
}
