using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class DatabaseProperties
{
    [JsonPropertyName("IsSyMSCDMDatabase")]
    public bool IsSyMSCDMDatabase { get; set; } = true;
}
