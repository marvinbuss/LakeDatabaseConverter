using System.Text.Json.Serialization;

namespace ModelConversionApp.Models;

internal class TableDescriptionProperties
{
    [JsonPropertyName("textinputformat.record.delimiter")]
    public string TextinputformatRecordDelimiter { get; set; } = ",";
    [JsonPropertyName("compression")]
    public string Compression { get; set; } = "{\"type\":\"None\",\"level\":\"optimal\"}";
    [JsonPropertyName("derivedModelAttributeInfo")]
    public string DerivedModelAttributeInfo { get; set; } = "{\"attributeReferences\":{}}";
}
