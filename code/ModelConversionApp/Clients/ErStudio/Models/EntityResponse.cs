using System.Text.Json.Serialization;

namespace ModelConversionApp.Clients.ErStudio.Models;

public class EntityMetadata
{
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
}

public class EntitySecurityproperty
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

public class EntitySubmodel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
}

public class EntityAttribute
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("default")]
    public string Default { get; set; }
    [JsonPropertyName("definition")]
    public string Definition { get; set; }
    [JsonPropertyName("keytype")]
    public string Keytype { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("domain")]
    public string Domain { get; set; }
    [JsonPropertyName("isNullable")]
    public string IsNullable { get; set; }
    [JsonPropertyName("datatype")]
    public string Datatype { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Entity
{
    [JsonPropertyName("diagramURL")]
    public string DiagramURL { get; set; }
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("schema")]
    public string Schema { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("modelURL")]
    public string ModelURL { get; set; }
    [JsonPropertyName("alerts")]
    public List<object> Alerts { get; set; }
    [JsonPropertyName("tableName")]
    public string TableName { get; set; }
    [JsonPropertyName("createdAt")]
    public int CreatedAt { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("modelLink")]
    public string ModelLink { get; set; }
    [JsonPropertyName("diagramLink")]
    public string DiagramLink { get; set; }
    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; set; }
    [JsonPropertyName("diagramId")]
    public int DiagramId { get; set; }
    [JsonPropertyName("securityproperties")]
    public List<EntitySecurityproperty> EntitySecurityproperties { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("modelId")]
    public int ModelId { get; set; }
    [JsonPropertyName("submodels")]
    public List<EntitySubmodel> EntitySubmodels { get; set; }
    [JsonPropertyName("diagram")]
    public string Diagram { get; set; }
    [JsonPropertyName("attributes")]
    public List<EntityAttribute> EntityAttributes { get; set; }
    [JsonPropertyName("notes")]
    public string Notes { get; set; }
}

public class EntityResponse
{
    [JsonPropertyName("metadata_")]
    public EntityMetadata EntityMetadata { get; set; }
    [JsonPropertyName("entities")]
    public List<Entity> Entities { get; set; }
}
