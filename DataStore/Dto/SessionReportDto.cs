﻿namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal class SessionReportDto : IDataTableObject
    {
        public int SessionReportId { get; set; }
        public int SessionId { get; set; }
        public bool IsStarted { get; set; }
        public bool IsConnected { get; set; }
        public int TotalRequestCount { get; set; }
        public int FailedRequestCount { get; set; }
        public string HubNames { get; set; }
        public int TotalConnectionTime { get; set; }
        public string NegotiationData { get; set; }

        public string TableName => "SessionReport";
    }
}
