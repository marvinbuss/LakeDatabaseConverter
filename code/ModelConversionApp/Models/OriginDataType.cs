using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class OriginDataType
{
    [JsonPropertyName("TypeName")]
    public string TypeName { get; set; }
    [JsonPropertyName("IsComplexType")]
    public bool IsComplexType { get; set; } = false;
    [JsonPropertyName("IsNullable")]
    public bool IsNullable { get; set; } = true;
    [JsonPropertyName("Length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Length { get; set; } = 0;
    [JsonPropertyName("Properties")]
    public OriginDataTypeProperties Properties { get; set; }
}
