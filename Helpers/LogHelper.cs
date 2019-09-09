using Newtonsoft.Json;
using System;
using System.IO;

namespace Sahurjt.Signalr.Dashboard.Helpers
{
    public class LogHelper
    {
        private static bool enableDebug = true;
        private static readonly string logFilePath = @"c:\db\logs.log";
        private static readonly StreamWriter logger = File.AppendText(logFilePath);


        public static void Log(string message)
        {

            if (enableDebug)
            {
                var logText = $"--- {DateTime.Now} | {message} ---";
                System.Diagnostics.Debug.WriteLine(logText);
                logger.WriteLine(logText);
                logger.Flush();
            }
        }

        public static void Log(object message)
        {
            var stringMessage = " ?? ";
            try
            {
                stringMessage = "JSON : " + JsonConvert.SerializeObject(message);
            }
            finally
            {
                Log(stringMessage);
            }
        }

        public static void Log(string message, params object[] args)
        {
            Log(message + " : " + string.Join(" , ", args ?? new string[] { }));
        }

        public static void SetLogging(bool enabled = true)
        {
            enableDebug = enabled;
        }
    }
}
