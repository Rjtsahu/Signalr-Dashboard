using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Sahurjt.Signalr.Dashboard.Extensions
{
    internal static class SqlReaderExtensions
    {
        public static T ToObject<T>(this DbDataReader reader)
        {
            if (reader == null || !reader.Read()) return default(T);

            return DbDataReaderToObject<T>(reader);
        }

        public static IList<T> ToList<T>(this DbDataReader reader)
        {
            IList<T> items = new List<T>();

            while (reader.Read())
            {
                items.Add(DbDataReaderToObject<T>(reader));
            }
            return items;
        }


        private static T DbDataReaderToObject<T>(DbDataReader reader)
        {
            var obj = Activator.CreateInstance<T>();
            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).Select(s=>s.ToLower()).ToList();
            var fields = typeof(T).GetProperties().Where(w=> columns.Contains(w.Name.ToLower()) ).ToList();

            foreach (var fieldInfo in fields)
            {
                try
                {
                    fieldInfo.SetValue(obj, Convert.ChangeType(reader[fieldInfo.Name],fieldInfo.PropertyType));
                }
                catch { }
            }

            return obj;
        }


    }
}
