using System.Collections.Generic;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal class SqliteQueryProvider : ISqlQueryProvider
    {

        public string DatabaseProviderName => "SQLite3";

        public IDictionary<ExecuteSqlQuery, string> ExecuteSqls => new Dictionary<ExecuteSqlQuery, string> {
            { ExecuteSqlQuery.Create_DatabaseTables , _createTableQuery },
            { ExecuteSqlQuery.InsertRow_Request ,_insertIntoRequestQuery },
            { ExecuteSqlQuery.InsertRow_Session,_insertIntoSessionQuery},
            { ExecuteSqlQuery.InsertRow_SessionReport,_insertIntoSessionReportQuery},
            { ExecuteSqlQuery.InsertRow_HubData,_insertIntoHubDataQuery},

            { ExecuteSqlQuery.Update_SessionOnCompleted,_updateSessionWhenCompletedQuery},
            { ExecuteSqlQuery.Update_RequestOnCompleted,_updateRequestWhenCompletedQuery},
        };

        public IDictionary<SelectSqlQuery, string> SelectSqls => new Dictionary<SelectSqlQuery, string> {
            {SelectSqlQuery.GetAll_Request , "SELECT * FROM Request " },
            {SelectSqlQuery.GetAll_Session , "SELECT * FROM Session " },
            {SelectSqlQuery.GetAll_SessionReport , "SELECT * FROM SessionReport " },
            {SelectSqlQuery.GetAll_HubData , "SELECT * FROM HubData " },

            {SelectSqlQuery.GetSingle_Session_By_ConnectionToken , _getSessionWithConnectionToken}
        };

        public string GetSql(ExecuteSqlQuery executeSqlEnum)
        {
            if (ExecuteSqls.ContainsKey(executeSqlEnum))
            {
                return ExecuteSqls[executeSqlEnum];
            }
            throw new KeyNotFoundException($" {DatabaseProviderName} doesn't provide exec sql query for enum: {executeSqlEnum.ToString()}");
        }

        public string GetSql(SelectSqlQuery selectSqlEnum)
        {
            if (SelectSqls.ContainsKey(selectSqlEnum))
            {
                return SelectSqls[selectSqlEnum];
            }
            throw new KeyNotFoundException($" {DatabaseProviderName} doesn't provide select sql query for enum: {selectSqlEnum.ToString()}");
        }

        #region DDL Queries

        /// <summary>
        /// Create database schema query
        /// </summary>
        private const string _createTableQuery = @"
                --  sqlite schema for middleware
                PRAGMA foreign_keys = ON;

                CREATE TABLE IF NOT EXISTS Session
                (
	                SessionId INTEGER PRIMARY KEY AUTOINCREMENT, 
	                ConnectionToken TEXT,
	                ConnectionId TEXT,
	                IsCompleted INTEGER DEFAULT 0,
	                StartTimeStamp DATETIME DEFAULT CURRENT_TIMESTAMP,
	                FinishTimeStamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                    NegotiateData TEXT
                );

                CREATE TABLE IF NOT EXISTS SessionReport
                (
	                SessionReportId INTEGER PRIMARY KEY AUTOINCREMENT,
	                SessionId INTEGER,
	                IsStarted INTEGER DEFAULT 0,
	                IsConnected INTEGER DEFAULT 0,
	                TotalRequestCount INTEGER,
	                FailedRequestCount INTEGER,
	                HubNames TEXT,
	                TotalConnectionTime DATETIME DEFAULT 0,
	                NegotiationData TEXT,
	                FOREIGN KEY(SessionId) REFERENCES Session(SessionId)
                );

                CREATE TABLE IF NOT EXISTS Request
                (
	                RequestId INTEGER PRIMARY KEY AUTOINCREMENT,
                    OwinRequestId TEXT UNIQUE NOT NULL,
	                SessionId INTEGER NULL,
	                RequestUrl TEXT,
	                RemoteIp TEXT,
	                RemotePort INTEGER,
	                ServerIp TEXT,
	                ServerPort INTEGER,
	                RequestContentType TEXT,
	                RequestBody TEXT,
	                Protocol TEXT,
	                QueryString TEXT,
	                User TEXT,
	                RequestTimeStamp DATETIME DEFAULT CURRENT_TIMESTAMP,
	                ResponseTimeStamp DATETIME DEFAULT CURRENT_TIMESTAMP, 
	                RequestLatency DATETIME,
	                StatusCode INTEGER,
	                ResponseBody TEXT,
	                IsWebSocketRequest INTEGER DEFAULT 0,
	                RequestType TEXT NULL, -- type of request : /send , /connect , /start , /negotiate , /abort , /reconnect
	                FOREIGN KEY(SessionId) REFERENCES Session(SessionId)
                );

                CREATE TABLE IF NOT EXISTS HubData
                (
	                Id INTEGER PRIMARY KEY AUTOINCREMENT,
	                RequestId INTEGER,
	                HubName TEXT,
	                MethodName TEXT,
	                Arguments TEXT,
	                ReturnData TEXT,
	                ExceptionData TEXT,
	                FOREIGN KEY(RequestId) REFERENCES Request(RequestId)
                );
        ";
        #endregion

        #region Insert queries

        private const string _insertIntoRequestQuery = @"INSERT INTO Request (OwinRequestid , SessionId, RequestUrl, RemoteIp, RemotePort, ServerIp, ServerPort, RequestContentType, RequestBody, Protocol, 
                                                      QueryString, User, RequestTimeStamp, ResponseTimeStamp, RequestLatency, StatusCode, ResponseBody, IsWebSocketRequest, RequestType) 
                                                      VALUES (@OwinRequestId, @SessionId, @RequestUrl, @RemoteIp, @RemotePort, @ServerIp, @ServerPort, @RequestContentType, @RequestBody, @Protocol, @QueryString, @User,
                                                      @RequestTimeStamp, @ResponseTimeStamp, @RequestLatency, @StatusCode, @ResponseBody, @IsWebSocketRequest, @RequestType)";

        private const string _insertIntoSessionQuery = @" INSERT INTO Session ( ConnectionId,ConnectionToken, IsCompleted, StartTimeStamp, FinishTimeStamp , NegotiateData)
                                                        VALUES ( @ConnectionId,@ConnectionToken, @IsCompleted, @StartTimeStamp, @FinishTimeStamp , @NegotiateData)";

        private const string _insertIntoSessionReportQuery = @" INSERT INTO SessionReport (SessionId, IsStarted, IsConnected, TotalRequestCount, FailedRequestCount, HubNames, TotalConnectionTime, NegotiationData)
                                                         VALUES  ( @SessionId, @IsStarted, @IsConnected, @TotalRequestCount, @FailedRequestCount, @HubNames, @TotalConnectionTime, @NegotiationData ) ";

        private const string _insertIntoHubDataQuery = @" INSERT INTO HubData (RequestId, HubName, MethodName, Arguments, ReturnData, ExceptionData )
                                                        VALUES ( @RequestId, @HubName, @MethodName, @Arguments, @ReturnData, @ExceptionData ) ";
        #endregion

        #region Update Queries

        private const string _updateSessionWhenCompletedQuery = @"UPDATE Session SET IsCompleted = @IsCompleted , 
                                                                       FinishTimeStamp = @FinishTimeStamp  WHERE ConnectionToken = @ConnectionToken ;";

        private const string _updateRequestWhenCompletedQuery = @"UPDATE Request SET ResponseTimeStamp = @ResponseTimeStamp , RequestLatency = @RequestLatency,
                                                                    StatusCode = @StatusCode , ResponseBody = @ResponseBody WHERE OwinRequestId = @OwinRequestId";
        #endregion

        #region Select Queries

        private const string _getSessionWithConnectionToken = @" SELECT * FROM Session WHERE ConnectionToken = @ConnectionToken";

        #endregion

    }
}
