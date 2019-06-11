using System;

namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal static class DtoExtensions
    {
        public static bool AsBoolean(this IDataTableObject _, object data)
        {
            if (Boolean.TryParse((string)data, out bool result)) return result;

            return default(bool);
        }

        public static DateTime AsDateTime(this IDataTableObject _, long epochTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epochTime);
        }
    }
}
