namespace ModelConversionApp.Models
{
    internal class TableProperties
    {
        private readonly string Description;
        private readonly string DisplayFolderInfo;
        private readonly string PrimaryKeys;
        //private readonly string spark.sql.sources.provider;

        public TableProperties(string description, string displayFolderInfo, string primaryKeys)
        {
            this.Description = description;
            this.DisplayFolderInfo = displayFolderInfo;
            this.PrimaryKeys = primaryKeys;
            // this.spark.sql.sources.provider = "parquet";
        }
    }
}