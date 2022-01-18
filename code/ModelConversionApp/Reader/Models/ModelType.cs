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
#pragma warning disable CS8603 // Possible null reference return.
        return type switch
        {
            ModelType.ErStudio => new ErStudioLoader(),
            _ => null,
        };
#pragma warning restore CS8603 // Possible null reference return.
    }
}
