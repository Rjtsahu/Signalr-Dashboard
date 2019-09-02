using Sahurjt.Signalr.Dashboard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal static class DtoExtensions
    {
        public static bool AsBoolean(this IDataTableObject _, object data)
        {
            if (bool.TryParse((string)data, out bool result)) return result;

            return default(bool);
        }

        public static DateTime AsDateTime(this IDataTableObject _, long epochTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epochTime);
        }

        public static long AsEpoch(this IDataTableObject _, DateTime dateTime)
        {
            return (long)dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        public static List<string> CsvAsList(this IDataTableObject _, string csvString)
        {
            if (string.IsNullOrEmpty(csvString))
            {
                return new List<string>();
            }
            return csvString.Split(',').ToList();
        }

        public static IEnumerable<TEntity> GetAll<TEntity>(this TEntity entity) where TEntity : IDataTableObject
        {
            var enumName = $"GetAll_{entity.TableName}";
            if (Enum.TryParse<SelectSqlQuery>(enumName, out var queryEnum))
            {
                ISqlOperation dep = DashboardGlobal.ServiceResolver.GetService<ISqlOperation>();

                return dep.SelectMultiple<TEntity>(queryEnum);
            }

            throw new NotImplementedException($"No SelectSqlQuery enum found for table name: {entity.TableName} , enum name: {enumName} .");
        }

        public static TEntity GetSingle<TEntity>(this TEntity entity, int primaryId) where TEntity : IDataTableObject
        {
            var enumName = $"GetSingle_{entity.TableName}";
            if (Enum.TryParse<SelectSqlQuery>(enumName, out var queryEnum))
            {
                ISqlOperation dep = DashboardGlobal.ServiceResolver.GetService<ISqlOperation>();

                return dep.Select<TEntity>(queryEnum, primaryId);
            }

            throw new NotImplementedException($"No SelectSqlQuery enum found for table name: {entity.TableName} , enum name: {enumName} .");
        }

    }
}
