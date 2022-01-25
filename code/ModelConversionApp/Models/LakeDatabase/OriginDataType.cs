using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class OriginDataType
{
    [JsonPropertyName("TypeName")]
    public string TypeName { get; set; } = string.Empty;
    [JsonPropertyName("IsComplexType")]
    public bool IsComplexType { get; set; } = false;
    [JsonPropertyName("IsNullable")]
    public string IsNullable { get; set; } = "true";
    [JsonPropertyName("Length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Length { get; set; } = "0";
    [JsonPropertyName("Properties")]
    public OriginDataTypeProperties? Properties { get; set; }
}
