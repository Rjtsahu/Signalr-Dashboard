namespace Sahurjt.Signalr.Dashboard.DataStore
{
    // Use Prefix "GetAll_" for fetching all records from a table.
    internal enum SelectSqlQuery
    {
        GetAll_Request,
        GetAll_Session,
        GetAll_HubData,
        GetAll_SessionReport,
        GetSingle_Request,
        GetSingle_Session,
        GetSingle_HubData,
        GetSingle_SessionReport
    }

    // Use Prefix "InsertRow_" for inserting query for a table.
    internal enum ExecuteSqlQuery
    {
        Create_DatabaseTables,
        InsertRow_Request,
        InsertRow_Session,
        InsertRow_HubData,
        InsertRow_SessionReport
    }
}
