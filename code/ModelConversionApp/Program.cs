using ModelConversionApp.Reader.Models;
using System;

namespace ModelConversionApp;

internal class Program
{
    static void Main(string[] args)
    {
        // Define file path
        var filePath = @"C:\\Users\\...";

        Utils.ReadXmlFile(filePath);

        // var loader = ModelTypeConverter.ConvertModelToLoader(type: ModelType.ErStudio);
        // loader.LoadModel(filePath: filePath);

        // var myXmlObject = Utils.ReadXmlFile(file_path);
        Console.WriteLine("Hello World");
    }
}