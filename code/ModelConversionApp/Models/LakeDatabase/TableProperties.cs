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

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public List<string> ForeignKeys { get; set; } = new List<string>();
    [JsonPropertyName("spark.sql.sources.provider")]
    public string SparkSqlSourcesProvider { get; set; } = "parquet";
}
