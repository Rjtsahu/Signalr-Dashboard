﻿using System.Collections.Generic;

namespace SignalrDashboard.DataStore
{
    interface ISqlQueryProvider
    {
        string DatabaseProviderName { get; }
        IDictionary<ExecuteSqlQuery, string> ExecuteSqls { get; }
        IDictionary<SelectSqlQuery, string> SelectSqls { get; }
        string GetSql(ExecuteSqlQuery executeSqlEnum);
        string GetSql(SelectSqlQuery selectSqlEnum);
    }
}
