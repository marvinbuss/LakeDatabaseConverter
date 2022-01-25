using ModelConversionApp.Models.Reader;
using ModelConversionApp.Writer;

namespace ModelConversionApp;

internal class Program
{
    static void Main(string[] args)
    {
        // Define file path
        var filePath = @"C:\Users\marvi\source\marvinbuss\SynapseModelConversion\code\models\export\model.dsv";
        
        // Convert Model to Table and Relationship objects
        var loader = ModelTypeConverter.ConvertModelToLoader(type: ModelType.ErStudio, filePath: filePath);
        var (tables, relationships) = loader.LoadModel();

        // Write table and relationship objects as lake databases
        var writer = new LakeDatabaseWriter(tables: tables, relationships: relationships);
        writer.WriteLakeDatabase();
    }
}