namespace ModelConversionApp.Models;

internal class TableDescriptionProperties
{
    // private readonly string textinputformat.record.delimiter;
    private readonly string compression;
    private readonly string derivedModelAttributeInfo;
    
    public TableDescriptionProperties()
    {
        // this.textinputformat.record.delimiter = ",";
        this.compression = "{\"type\":\"None\",\"level\":\"optimal\"}";
        this.derivedModelAttributeInfo = "{\"attributeReferences\":{}}";
    }
}
