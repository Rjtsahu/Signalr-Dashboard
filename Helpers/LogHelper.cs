using Newtonsoft.Json;
using System;

namespace Sahurjt.Signalr.Dashboard.Helpers
{
    public class LogHelper
    {
        private static readonly bool enableDebug = true;

        public static void Log(string message)
        {
            if (enableDebug)
            {
                var time = DateTime.Now;
                System.Diagnostics.Debug.WriteLine($"--- {time} | {message} ---");
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


    }
}
