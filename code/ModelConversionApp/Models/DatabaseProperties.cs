namespace ModelConversionApp.Models;

internal class DatabaseProperties
{
    private readonly bool IsSyMSCDMDatabase;

    public DatabaseProperties(bool isSyMSCDMDatabase)
    {
        this.IsSyMSCDMDatabase = isSyMSCDMDatabase;
    }
}
