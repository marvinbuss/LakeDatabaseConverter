namespace ModelConversionApp.Reader.Loader;

internal interface ILoader
{
    internal Task LoadModelAsync(string filePath);
}
