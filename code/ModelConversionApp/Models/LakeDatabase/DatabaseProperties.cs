using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class DatabaseProperties
{
    [JsonPropertyName("IsSyMSCDMDatabase")]
    public bool IsSyMSCDMDatabase { get; set; } = true;
}
