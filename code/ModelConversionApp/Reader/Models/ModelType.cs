using ModelConversionApp.Reader.Loader;

namespace ModelConversionApp.Reader.Models;

internal enum ModelType
{
    ErStudio
}

internal static class ModelTypeConverter
{
    internal static ILoader ConvertModelToLoader(ModelType type)
    {
        return type switch
        {
            ModelType.ErStudio => new ErStudioLoader(),
            _ => throw new NotSupportedException()
        };
    }
}
