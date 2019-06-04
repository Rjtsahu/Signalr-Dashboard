using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    public interface ISqlOperation
    {
        T Select<T>(string selectSql, params object[] parameters);
        IList<T> SelectMultiple<T>(string selectSql, params object[] parameters);

        int Execute(string executeSql, params object[] parameters);
        Task<int> ExecuteAsync(string executeSql, params object[] parameters);
    }
}
