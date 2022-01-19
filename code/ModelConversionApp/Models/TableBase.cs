namespace ModelConversionApp.Models
{
    internal class TableBase
    {
        private readonly string Name;
        private readonly string Description;
        private readonly string Version;
        private readonly string EntityType;
        private readonly string TableType;
        private readonly int Retention;
        private readonly bool Temporary;
        private readonly bool IsRewriteEnabled;
        private readonly TableNamespace Namespace;
        private readonly TableProperties Properties;
        private readonly Origin Origin;
        private readonly TableDescription StorageDescriptor;

        internal void AddColumn(Column column)
        {
            this.StorageDescriptor.AddColumn(column);
        }
    }
}