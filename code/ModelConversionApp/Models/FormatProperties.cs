namespace ModelConversionApp.Models;

internal class FormatProperties
{
    private readonly string path;
    private readonly bool FormatTypeSetToDatabaseDefault;

    public FormatProperties()
    {
        this.path = "";
        this.FormatTypeSetToDatabaseDefault = false;
    }
}
