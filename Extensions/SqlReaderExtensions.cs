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
            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).Select(s => s.ToLower()).ToList();
            var fields = obj.GetType().GetFields().Select(s => s.Name.ToLower()).ToList();

            var commonFieldNames = columns.Concat(fields);

            foreach (var fieldName in commonFieldNames)
            {
                try
                {
                    obj.GetType().GetField(fieldName).SetValue(obj, reader[fieldName]);
                }
                catch { }
            }

            return obj;
        }


    }
}
