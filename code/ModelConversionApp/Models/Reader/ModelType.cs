using ModelConversionApp.Reader;

namespace ModelConversionApp.Models.Reader;

internal enum ModelType
{
    ErStudio
}

internal static class ModelTypeConverter
{
    internal static ILoader ConvertModelToLoader(ModelType type, FileInfo fileInfo)
    {
        return type switch
        {
            ModelType.ErStudio => new ErStudioLoader(fileInfo: fileInfo),
            _ => throw new NotSupportedException()
        };
    }
}
