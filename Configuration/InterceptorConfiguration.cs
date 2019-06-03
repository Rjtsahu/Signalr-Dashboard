using System;

namespace Sahurjt.Signalr.Dashboard.Configuration
{
    public class InterceptorConfiguration
    {
        private TimeSpan _FlushOldRecordAfter = TimeSpan.FromDays(3);


        public TimeSpan FlushOldRecordAfter { get { return _FlushOldRecordAfter; } set { _FlushOldRecordAfter = value; } }

        public string ConnectionString { get; set; }

        public bool UseSqlServerStorage { get; set; } = false;
     
    }
}
