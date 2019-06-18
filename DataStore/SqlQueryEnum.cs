namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal enum SelectSqlQuery
    {
        GetAll_Request,
        GetAll_Session,
        GetAll_HubData,
        GetAll_SessionReport
    }

    internal enum ExecuteSqlQuery
    {
        Create_DatabaseTables,
        InsertRow_Request,
        InsertRow_Session,
        InsertRow_HubData,
        InsertRow_SessionReport
    }
}
