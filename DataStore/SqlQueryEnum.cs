namespace SignalrDashboard.DataStore
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
        GetSingle_SessionReport,

        GetSingle_Session_By_ConnectionToken
    }

    // Use Prefix "InsertRow_" for inserting query for a table.
    internal enum ExecuteSqlQuery
    {
        /// <summary>
        /// Emum to create database schema, will be used on first runs.
        /// </summary>
        Create_DatabaseTables,

        /// <summary>
        /// Insert record into Request table.
        /// See : <see cref="Dto.RequestDto"/>
        /// <para>
        /// Must have paramters as following : 
        ///  SessionId, RequestUrl, RemoteIp, RemotePort, ServerIp, ServerPort, RequestContentType, RequestBody, Protocol, 
        ///  QueryString, User, RequestTimeStamp, ResponseTimeStamp, RequestLatency, StatusCode, ResponseBody, IsWebSocketRequest, RequestType
        /// </para>
        /// </summary>
        InsertRow_Request,

        /// <summary>
        /// Insert record into Session table.
        /// See : <see cref="Dto.SessionDto"/>
        /// <para>
        /// Must have paramters as following : 
        /// ConnectionId,ConnectionToken, IsCompleted, StartTimeStamp, FinishTimeStamp , NegotiateData
        /// </para>
        /// </summary>
        InsertRow_Session,

        /// <summary>
        /// Insert record into HubData table.
        /// See : <see cref="Dto.HubDataDto"/>
        /// <para>
        /// Must have paramters as following : 
        /// RequestId, HubName, MethodName, Arguments, ReturnData, ExceptionData
        /// </para>
        /// </summary>
        InsertRow_HubData,

        /// <summary>
        /// Insert record into SessionReport table.
        /// See : <see cref="Dto.SessionReportDto"/>
        /// <para>
        /// Must have paramters as following : 
        /// SessionId, IsStarted, IsConnected, TotalRequestCount, FailedRequestCount, HubNames, TotalConnectionTime, NegotiationData
        /// </para>
        /// </summary>
        InsertRow_SessionReport,

        /// <summary>
        /// Enum used to update record of Session table when a client completes its session.
        /// <para>
        /// Order of parameters : 
        /// IsCompleted , FinishTimeStamp | ConnectionToken (for selection)
        /// </para>
        /// </summary>
        Update_SessionOnCompleted,

        /// <summary>
        /// Enum used to update record of Request table when a client completes its particular type of http request.
        /// <para>
        /// Order of parameters : 
        /// ResponseTimeStamp , RequestLatency , StatusCode , ResponseBody , OwinRequestId
        /// </para>
        /// </summary>
        Update_RequestOnCompleted
    }
}
