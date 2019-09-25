using System.Data.SQLite;
using SignalrDashboard.Extensions;
using System.Data;

namespace SignalrDashboard.DataStore
{
    internal class SqliteOperation : BaseSqlOperation
    {
        private string connectionString;

        public SqliteOperation(string connectionString) :
            base(new SqliteQueryProvider())
        {
            this.connectionString = connectionString;
        }

        public override string ProviderName => "Sqlite3";

        protected override IDbCommand GetCommandParameter(string sql, params object[] parameters)
        {
            var sqlCommand = new SQLiteCommand();
            sqlCommand.Parameters.MapSQLiteParameters(sql, parameters);
        
            return sqlCommand;
        }

        protected override IDbConnection GetDbConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
