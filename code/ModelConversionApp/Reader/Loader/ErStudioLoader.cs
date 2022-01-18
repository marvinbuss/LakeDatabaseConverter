using System.Xml;
using System.Xml.Schema;

namespace ModelConversionApp.Reader.Loader;
internal class ErStudioLoader : ILoader
{
    public void LoadModel(string filePath)
    {
        XmlSchema? schema = null;
        try
        {
            // Parse XML file to get XML schema definition
            var doc = new XmlDocument();
            doc.Load(filename: filePath);

            // Create XML namespace manager for resolving namespaces
            var nsMgr = new XmlNamespaceManager(doc.NameTable);
            nsMgr.AddNamespace("dataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine");
            nsMgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

            // Select XML node with schema definition
            var schemaNode = doc.SelectSingleNode(xpath: "//dataSourceView:Schema/xs:schema", nsmgr: nsMgr);
            var xmlStream = Utils.StringToStream(input: schemaNode.InnerXml);

            // Read XML schema
            schema = XmlSchema.Read(stream: xmlStream, validationEventHandler: ValidationCallback);

            // var reader = new XmlTextReader()
            // schema = XmlSchema.Read(schemaNode.InnerXml);
            // var reader = new XmlTextReader(filePath);
            // schema = XmlSchema.Read(reader: reader, validationEventHandler: ValidationCallback);
            // schema.Write(Console.Out);
            Console.WriteLine("Hello World");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while loading the Model schema definition: '{ex.Message}'");
        }
    }

    private static void ValidationCallback(object sender, ValidationEventArgs args)
    {
        if (args.Severity == XmlSeverityType.Warning)
        {
            Console.Write($"WARNING: {args.Message}");
        }
        else if (args.Severity == XmlSeverityType.Error)
        {
            Console.Write($"ERROR: {args.Message}");
        }
    }
}
