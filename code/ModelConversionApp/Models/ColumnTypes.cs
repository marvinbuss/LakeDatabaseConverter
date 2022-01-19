namespace ModelConversionApp.Models;

internal enum ColumnType
{
    Long,
    String,
    Timestamp
}

internal static class ColumnTypeConverter
{
    internal static string ConvertColumnTypeToString(ColumnType type)
    {
        return type switch
        {
            ColumnType.Long => "long",
            ColumnType.String => "string",
            ColumnType.Timestamp => "timestamp",
            _ => throw new NotSupportedException()
        };
    }
}
