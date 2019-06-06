using System.Collections.Generic;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal class SqliteQueryProvider : ISqlQueryProvider
    {

        public string DatabaseProviderName => "SQLite3";

        public IDictionary<ExecuteSqlQuery, string> ExecuteSqls => new Dictionary<ExecuteSqlQuery, string> {
            {ExecuteSqlQuery.InsertRecord__Request ,"insert into dummy_table (@a,@b)" }
        };

        public IDictionary<SelectSqlQuery, string> SelectSqls => new Dictionary<SelectSqlQuery, string> {
            {SelectSqlQuery.GetAll__Request , "select * from dummy_table where col = @col" }
        };

        public string GetSql(ExecuteSqlQuery executeSqlEnum)
        {
            if (ExecuteSqls.ContainsKey(executeSqlEnum))
            {
                return ExecuteSqls[executeSqlEnum];
            }
            throw new KeyNotFoundException($" {DatabaseProviderName} doesn't provide exec sql query for enum: {executeSqlEnum.ToString()}");
        }

        public string GetSql(SelectSqlQuery selectSqlEnum)
        {
            if (SelectSqls.ContainsKey(selectSqlEnum))
            {
                return SelectSqls[selectSqlEnum];
            }
            throw new KeyNotFoundException($" {DatabaseProviderName} doesn't provide select sql query for enum: {selectSqlEnum.ToString()}");
        }
    }
}
