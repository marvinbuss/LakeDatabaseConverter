namespace ModelConversionApp.Models;

internal class OriginDataType
{
    private readonly string TypeName;
    private readonly bool IsComplexType;
    private readonly bool IsNullable;
    private readonly int Length;
    private readonly OriginDataTypeProperties Properties;

    public OriginDataType(ColumnType type, bool isComplexType, bool isNullable, int length)
    {
        this.TypeName = ColumnTypeConverter.ConvertColumnTypeToString(type: type);
        this.IsComplexType = isComplexType;
        this.IsNullable = isNullable;
        this.Length = length;
        this.Properties = new OriginDataTypeProperties(hiveTypeString: ColumnTypeConverter.ConvertColumnTypeToString(type: type));
    }
}
