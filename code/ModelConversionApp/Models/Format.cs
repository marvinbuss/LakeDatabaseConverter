using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class Format
{
    [JsonPropertyName("InputFormat")]
    public string InputFormat { get; set; } = "org.apache.hadoop.hive.ql.io.parquet.MapredParquetInputFormat";
    [JsonPropertyName("OutputFormat")]
    public string OutputFormat { get; set; } = "org.apache.hadoop.hive.ql.io.parquet.MapredParquetOutputFormat";
    [JsonPropertyName("FormatType")]
    public string FormatType { get; set; }
    [JsonPropertyName("SerializeLib")]
    public string SerializeLib { get; set; } = "org.apache.hadoop.hive.ql.io.parquet.serde.ParquetHiveSerDe";
    [JsonPropertyName("Properties")]
    public FormatProperties Properties { get; set; } = new FormatProperties();
}
