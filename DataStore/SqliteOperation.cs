using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SQLite;
using Sahurjt.Signalr.Dashboard.Extensions;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal class SqliteOperation : ISqlOperation
    {

        public int Execute(ExecuteSqlQuery executeSql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteAsync(ExecuteSqlQuery executeSql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public T Select<T>(SelectSqlQuery selectSql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> SelectMultiple<T>(SelectSqlQuery selectSql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        private void ExecuteScalar()
        {
            using (var conn = new SQLiteConnection())
            {
                using (var cmd = new SQLiteCommand())
                {
                    var r = cmd.ExecuteReader();
                    cmd.Parameters.MapSQLiteParameters("", "");
                }
            }
        }


    }
}
