using ModelConversionApp.Models.LakeDatabase;

namespace ModelConversionApp.Reader;

internal interface ILoader
{
    internal (List<Table> tables, List<Relationship> relationships) LoadModel();
}
