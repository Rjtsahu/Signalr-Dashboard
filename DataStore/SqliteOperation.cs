using System.Data.SQLite;
using Sahurjt.Signalr.Dashboard.Extensions;
using System.Data.Common;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal class SqliteOperation : BaseSqlOperation
    {
        private string connectionString;

        public SqliteOperation(string connectionString) :
            base(new SqliteQueryProvider())
        {
            this.connectionString = connectionString;
        }

        protected override string ProviderName => "Sqlite3";

        protected override DbCommand GetCommandParameter(string sql, params object[] parameters)
        {
            var sqlCommand = new SQLiteCommand();
            sqlCommand.Parameters.MapSQLiteParameters(sql, parameters);
        
            return sqlCommand;
        }

        protected override DbConnection GetDbConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
