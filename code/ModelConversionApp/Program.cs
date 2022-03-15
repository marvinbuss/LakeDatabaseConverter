using ModelConversionApp.Models.Reader;
using ModelConversionApp.Writer;
using System.CommandLine;

namespace ModelConversionApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Define arguments
        var filePath = new Option<FileInfo>(
            name: "--file-path",
            description: "Specifies the file path of the model file.")
        {
            Arity = ArgumentArity.ExactlyOne,
            AllowMultipleArgumentsPerToken = false,
            ArgumentHelpName = "Model File Path",
            IsRequired = true
        }.ExistingOnly();
        filePath.AddAlias(alias: "-f");

        var modelType = new Option<ModelType>(
            name: "--model-type",
            description: "Specifies the type of the model.")
        {
            Arity = ArgumentArity.ExactlyOne,
            AllowMultipleArgumentsPerToken = false,
            ArgumentHelpName = "Model Type",
            IsRequired = true
        };
        modelType.AddAlias(alias: "-m");

        // Add root command
        var rootCommand = new RootCommand(description: "This Tool converts existing Data Models into Lake Database models.")
        {
            filePath,
            modelType
        };
        rootCommand.SetHandler((FileInfo filePath, ModelType modelType) =>
        {
            CreateLakeDatabase()
            Console.WriteLine($"The value for --bool-option is: {modelType}");
            Console.WriteLine($"The value for --file-option is: {filePath?.FullName ?? "null"}");
        }, filePath, modelType);

        await rootCommand.InvokeAsync(args);
    }

    static void CreateLakeDatabase(FileInfo fileInfo, ModelType modelType)
    {
        // Convert Model to Table and Relationship objects
        var loader = ModelTypeConverter.ConvertModelToLoader(type: modelType, fileInfo: fileInfo);
        var (tables, relationships) = loader.LoadModel();

        // Write table and relationship objects as lake databases
        var writer = new LakeDatabaseWriter(tables: tables, relationships: relationships);
        writer.WriteLakeDatabase();
    }
}