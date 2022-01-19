namespace ModelConversionApp.Models;

internal class OriginDataTypeProperties
{
    private readonly string HIVE_TYPE_STRING;
    private readonly string TimestampFormat;

    public OriginDataTypeProperties(string hiveTypeString)
    {
        this.HIVE_TYPE_STRING = hiveTypeString;
        this.TimestampFormat = "YYYY-MM-DD HH:MM:SS.fffffffff";
    }
}
