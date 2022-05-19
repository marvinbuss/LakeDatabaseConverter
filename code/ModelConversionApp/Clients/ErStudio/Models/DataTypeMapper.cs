using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConversionApp.Clients.ErStudio.Models;

internal class DataTypeMapper
{
    internal static string ConvertErStudioToLakeDatabaseDataType(string dataType)
    {
        return dataType switch
        {
            "binary" => "binary",
            "boolean" => "boolean",
            "byte" => "byte",
            "decimal" => "decimal",
            "double" => "double",
            "float" => "float",
            "integer" => "integer",
            "long" => "long",
            "short" => "short",
            "varchar" => "string",
            "char" => "string",
            "text" => "string",
            "date" => "date",
            "timestamp" => "timestamp",
            _ => throw new NotSupportedException()
        };
    }
}
