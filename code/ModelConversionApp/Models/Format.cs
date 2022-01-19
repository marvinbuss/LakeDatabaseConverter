namespace ModelConversionApp.Models;

internal class Format
{
    private readonly string InputFormat;
    private readonly string OutputFormat;
    private readonly string FormatType;
    private readonly string SerializeLib;
    private readonly FormatProperties Properties;

    public Format(string formatType)
    {
        this.InputFormat = "org.apache.hadoop.hive.ql.io.parquet.MapredParquetInputFormat";
        this.OutputFormat = "org.apache.hadoop.hive.ql.io.parquet.MapredParquetOutputFormat";
        this.FormatType = formatType;
        this.SerializeLib = "org.apache.hadoop.hive.ql.io.parquet.serde.ParquetHiveSerDe";
        this.Properties = new FormatProperties();
    }
}
