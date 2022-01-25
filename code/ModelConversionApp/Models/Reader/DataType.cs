namespace ModelConversionApp.Models.Reader;

internal class DataType
{
    internal string TypeName { get; set; } = string.Empty;
    internal Restrictions Restrictions { get; set; } = new Restrictions();
}

internal class Restrictions
{
    internal int Length { get; set; } = 0;  // Only for columns of type string
    public int Precision { get; set; } = 0;  // Only for columns of type decimal
    public int Scale { get; set; } = 0;  // Only for columns of type decimal
    public string? TimestampFormat { get; set; } = null;  // for timestamp use "YYYY-MM-DD HH:MM:SS.fffffffff"
    public string? DateFormat { get; set; } = null;  // for date use "YYYY-MM-DD"
}

internal class DataTypeConverter
{
    internal static string ConvertErStudioToLakeDatabaseDataType(string dataType)
    {
        return dataType switch
        {
            "xs:binary" => "binary",
            "xs:boolean" => "boolean",
            "xs:byte" => "byte",
            "xs:decimal" => "decimal",
            "xs:double" => "double",
            "xs:float" => "float",
            "xs:integer" => "integer",
            "xs:long" => "long",
            "xs:short" => "short",
            "xs:string" => "string",
            "xs:date" => "date",
            "xs:dateTime" => "timestamp",
            _ => throw new NotSupportedException()
        };
    }
}
