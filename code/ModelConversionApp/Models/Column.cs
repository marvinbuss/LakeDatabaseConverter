namespace ModelConversionApp.Models;

internal class Column
{
    private readonly string Name;
    private readonly string Description;
    private readonly OriginDataType OriginDataTypeName;

    public Column(string name, string description, ColumnType type, bool isComplexType, bool isNullable, int length)
    {
        this.Name = name;
        this.Description = description;
        this.OriginDataTypeName = new OriginDataType(type: type, isComplexType: isComplexType, isNullable: isNullable, length: length);
    }
}
