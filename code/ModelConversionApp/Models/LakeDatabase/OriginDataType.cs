using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class OriginDataType
{
    [JsonPropertyName("TypeName")]
    public string TypeName { get; set; } = string.Empty;
    [JsonPropertyName("IsComplexType")]
    public bool IsComplexType { get; set; } = false;
    [JsonPropertyName("IsNullable")]
    public bool IsNullable { get; set; } = true;
    [JsonPropertyName("Length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Length { get; set; } = 0;  // Only for columns of type string
    [JsonPropertyName("Precision")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Precision { get; set; } = 0;  // Only for columns of type decimal
    [JsonPropertyName("Scale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Scale { get; set; } = 0;  // Only for columns of type decimal
    [JsonPropertyName("Properties")]
    public OriginDataTypeProperties? Properties { get; set; } = new OriginDataTypeProperties();
}
