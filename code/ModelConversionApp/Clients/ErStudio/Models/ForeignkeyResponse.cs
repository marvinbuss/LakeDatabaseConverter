using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelConversionApp.Clients.ErStudio.Models;

public class ForeignkeyResponse
{
    [JsonPropertyName("foreignkeys")]
    public List<Foreignkey> Foreignkeys { get; set; }
    [JsonPropertyName("metadata_")]
    public ForeignkeyMetadata ForeignkeyMetadata { get; set; }
}

public class ForeignkeyMetadata
{
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}

public class Foreignkey
{
    [JsonPropertyName("diagramLink")]
    public string DiagramLink { get; set; }
    [JsonPropertyName("ConstraintName")]
    public string ConstraintName { get; set; }
    [JsonPropertyName("notes")]
    public string Notes { get; set; }
    [JsonPropertyName("parentURL")]
    public string ParentURL { get; set; }
    [JsonPropertyName("attachments")]
    public List<object> Attachments { get; set; }
    [JsonPropertyName("diagramId")]
    public int DiagramId { get; set; }
    [JsonPropertyName("parentTableName")]
    public string ParentTableName { get; set; }
    [JsonPropertyName("modelId")]
    public int ModelId { get; set; }
    [JsonPropertyName("businessName")]
    public string BusinessName { get; set; }
    [JsonPropertyName("existence")]
    public string Existence { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("parentEntitName")]
    public string ParentEntitName { get; set; }
    [JsonPropertyName("logicalOnly")]
    public string LogicalOnly { get; set; }
    [JsonPropertyName("verbPhrase")]
    public string VerbPhrase { get; set; }
    [JsonPropertyName("childTableName")]
    public string ChildTableName { get; set; }
    [JsonPropertyName("securityproperties")]
    public List<object> Securityproperties { get; set; }
    [JsonPropertyName("createdAt")]
    public long CreatedAt { get; set; }
    [JsonPropertyName("logicalPhysical")]
    public string LogicalPhysical { get; set; }
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("childURL")]
    public string ChildURL { get; set; }
    [JsonPropertyName("diagramURL")]
    public string DiagramURL { get; set; }
    [JsonPropertyName("modelURL")]
    public string ModelURL { get; set; }
    [JsonPropertyName("parentLink")]
    public string ParentLink { get; set; }
    [JsonPropertyName("relationshipType")]
    public string RelationshipType { get; set; }
    [JsonPropertyName("childLink")]
    public string ChildLink { get; set; }
    [JsonPropertyName("modelLink")]
    public string ModelLink { get; set; }
    [JsonPropertyName("childEntityName")]
    public string ChildEntityName { get; set; }
    [JsonPropertyName("inverseVerbPhrase")]
    public string InverseVerbPhrase { get; set; }
    [JsonPropertyName("childId")]
    public int ChildId { get; set; }
    [JsonPropertyName("cardinality")]
    public string Cardinality { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("parentId")]
    public int ParentId { get; set; }
    [JsonPropertyName("diagram")]
    public string Diagram { get; set; }
    [JsonPropertyName("alerts")]
    public List<object> Alerts { get; set; }
    [JsonPropertyName("Type")]
    public string Type { get; set; }
    [JsonPropertyName("physicalOnly")]
    public string PhysicalOnly { get; set; }
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("submodels")]
    public List<ForeignKeySubmodel> ForeignKeySubmodels { get; set; }
}

public class ForeignKeySubmodel
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
