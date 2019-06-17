using System.Data.SQLite;
using Sahurjt.Signalr.Dashboard.Extensions;
using System.Data.Common;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal class SqliteOperation : BaseSqlOperation
    {
        public SqliteOperation(string connectionString) :
            base(new SQLiteConnection(connectionString), new SQLiteCommand(), new SqliteQueryProvider())
        {

        }
        protected override string ProviderName => "Sqlite3";

        protected override DbCommand AddSqlParameters(DbCommand dbCommand, string sql, params object[] parameters)
        {
            var sqlCommand = dbCommand as SQLiteCommand;
            sqlCommand.Parameters.MapSQLiteParameters(sql, parameters);

            return sqlCommand;
        }
    }
}
