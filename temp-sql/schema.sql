--  sqlite schema for middleware
PRAGMA foreign_keys = ON;

CREATE TABLE Session
(
	SessionId INTEGER PRIMARY KEY AUTOINCREMENT, 
	ConnectionToken TEXT,
	ConnectionId TEXT,
	IsCompleted INTEGER DEFAULT 0,
	StartTimeStamp DATETIME DEFAULT CURRENT_TIMESTAMP,
	FinishTimeStamp DATETIME DEFAULT CURRENT_TIMESTAMP
)

CREATE TABLE SessionReport
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
)

CREATE TABLE Request
(
	RequestId INTEGER PRIMARY KEY AUTOINCREMENT,
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
)

CREATE TABLE HubData
(
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	RequestId INTEGER,
	HubName TEXT,
	MethodName TEXT,
	Arguments TEXT,
	ReturnData TEXT,
	ExceptionData TEXT,
	FOREIGN KEY(RequestId) REFERENCES Request(RequestId)

)