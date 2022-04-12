using System.Text.Json.Serialization;

namespace ModelConversionApp.Clients.ErStudio.Models;

public class ModelSubmodel
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
    [JsonPropertyName("parentSubmodelId")]
    public string ParentSubmodelId { get; set; }
}

public class Model
{
    [JsonPropertyName("diagramURL")]
    public string DiagramURL { get; set; }
    [JsonPropertyName("securityproperties")]
    public List<object> Securityproperties { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("project")]
    public string Project { get; set; }
    [JsonPropertyName("style")]
    public string Style { get; set; }
    [JsonPropertyName("modelType")]
    public string ModelType { get; set; }
    [JsonPropertyName("submodels")]
    public List<ModelSubmodel> ModelSubmodels { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("diagramLink")]
    public string DiagramLink { get; set; }
    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; set; }
    [JsonPropertyName("diagram")]
    public string Diagram { get; set; }
    [JsonPropertyName("owners")]
    public List<string> Owners { get; set; }
    [JsonPropertyName("notation")]
    public string Notation { get; set; }
    [JsonPropertyName("diagramId")]
    public int DiagramId { get; set; }
    [JsonPropertyName("isCurrentUserFollowing")]
    public bool IsCurrentUserFollowing { get; set; }
}

public class ModelMetadata
{
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
}

public class ModelResponse
{
    [JsonPropertyName("models")]
    public List<Model> Models { get; set; }
    [JsonPropertyName("metadata_")]
    public ModelMetadata ModelMetadata { get; set; }
}
