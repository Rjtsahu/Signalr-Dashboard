using System;

namespace SignalrDashboard.Configuration
{
    public class InterceptorConfiguration
    {
        private readonly string _defaultSignalrRoute = "/signalr";

        private static readonly string _defaultDashboardRoute = "/dashboard";

        private static readonly string _databaseConnectionString = "Data Source=C:\\db\\sample.db;Version=3;New=True;";


        public string DefaultSignalrRoute { get; internal set; }

        public string DefaultDashboardRoute { get; internal set; }

        public TimeSpan FlushOldRecordAfter { get; set; }

        public string ConnectionString { get; internal set; }

        public InterceptorConfiguration()
        {
            FlushOldRecordAfter = TimeSpan.FromDays(3);
            DefaultSignalrRoute = _defaultSignalrRoute;
            DefaultDashboardRoute = _defaultDashboardRoute;
            ConnectionString = _databaseConnectionString;
        }
    }
}
