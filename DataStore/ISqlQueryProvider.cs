namespace Sahurjt.Signalr.Dashboard.DataStore
{
    interface ISqlQueryProvider
    {
        string DatabaseProviderName { get; }
        string GetExecuteSql(ExecuteSqlQuery executeSqlEnum);
        string GetSelectSql(SelectSqlQuery selectSqlEnum);
    }
}
