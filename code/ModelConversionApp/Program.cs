using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelConversionApp.Clients.ErStudio;
using ModelConversionApp.Models.Reader;
using ModelConversionApp.Writer;
using System.CommandLine;

namespace ModelConversionApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Parse args
        var rootCommand = ParseArgs();

        // Initialize Services
        var host = ConfigureServices();
        using (var serviceScope= host.Services.CreateScope())
        {
            await rootCommand.InvokeAsync(args);
        }
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

    static RootCommand ParseArgs()
    {
        // Define argument for model file path
        var fileInfo = new Option<FileInfo>(
            name: "--file-path",
            description: "Specifies the file path of the model file.")
        {
            Arity = ArgumentArity.ExactlyOne,
            AllowMultipleArgumentsPerToken = false,
            ArgumentHelpName = "Model File Path",
            IsRequired = true
        }.ExistingOnly();
        fileInfo.AddAlias(alias: "-f");

        // Define argument for model type
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
            fileInfo,
            modelType
        };
        rootCommand.SetHandler((FileInfo fileInfo, ModelType modelType) =>
        {
            CreateLakeDatabase(fileInfo: fileInfo, modelType: modelType);
        }, fileInfo, modelType);

        return rootCommand;
    }

    static IHost ConfigureServices()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(c => c.AddConsole());
                services.AddTransient<ErStudioClient>();
            })
            .UseConsoleLifetime()
            .Build();

        return host;
    }
}