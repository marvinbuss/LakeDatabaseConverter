using System.Text.Json.Serialization;

namespace ModelConversionApp.Models.LakeDatabase;

internal class TableProperties
{
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "";
    [JsonPropertyName("DisplayFolderInfo")]
    public string DisplayFolderInfo { get; set; } = "{\"name\":\"Others\",\"colorCode\":\"\"}";
    [JsonPropertyName("PrimaryKeys")]
    public string PrimaryKeys { get; set; } = "";
    [JsonPropertyName("spark.sql.sources.provider")]
    public string sparkSqlSourcesProvider { get; set; } = "parquet";
}
