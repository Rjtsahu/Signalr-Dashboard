using System;
namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal class SqliteQueryProvider : ISqlQueryProvider
    {
        private readonly string InsertRecord__Request = "insert into dummy_table (@a,@b)";
        private readonly string GetAll__Request = "select * from dummy_table where col = @col";


        public string DatabaseProviderName => "SQLite3";


        public string GetExecuteSql(ExecuteSqlQuery executeSqlEnum)
        {
            var query = "";
            switch (executeSqlEnum)
            {
                case ExecuteSqlQuery.InsertRecord__Request:
                    query = InsertRecord__Request;
                    break;
                default:
                    throw new ArgumentException($" {DatabaseProviderName} doesn't provide exec sql query for enum: {executeSqlEnum.ToString()}");
            }
            return query;
        }

        public string GetSelectSql(SelectSqlQuery selectSqlEnum)
        {
            var query = "";
            switch (selectSqlEnum)
            {
                case SelectSqlQuery.GetAll__Request:
                    query = GetAll__Request;
                    break;
                default:
                    throw new ArgumentException($" {DatabaseProviderName} doesn't provide select sql query for enum: {selectSqlEnum.ToString()}");
            }
            return query;
        }
    }
}
