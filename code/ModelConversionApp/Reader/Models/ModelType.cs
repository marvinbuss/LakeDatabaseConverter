using ModelConversionApp.Reader.Loader;

namespace ModelConversionApp.Reader.Models;

internal enum ModelType
{
    ErStudio
}

internal static class ModelTypeConverter
{
    internal static ILoader ConvertModelToLoader(ModelType type, string filePath)
    {
        return type switch
        {
            ModelType.ErStudio => new ErStudioLoader(filePath: filePath),
            _ => throw new NotSupportedException()
        };
    }
}
