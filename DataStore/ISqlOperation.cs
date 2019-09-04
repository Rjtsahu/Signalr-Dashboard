using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal interface ISqlOperation
    {
        T Select<T>(SelectSqlQuery selectSql, params object[] parameters);
        IList<T> SelectMultiple<T>(SelectSqlQuery selectSql, params object[] parameters);

        T Select<T>(string selectRawSql, params object[] parameters);
        IList<T> SelectMultiple<T>(string selectRawSql, params object[] parameters);

        int Execute(ExecuteSqlQuery executeSql, params object[] parameters);
        Task<int> ExecuteAsync(ExecuteSqlQuery executeSql, params object[] parameters);

        int Execute(string executeRawSql, params object[] parameters);
        Task<int> ExecuteAsync(string executeRawSql, params object[] parameters);

    }
}
