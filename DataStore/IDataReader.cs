using System.Collections.Generic;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    interface IDataReader<T>
    {
        T Get();
        IEnumerable<T> GetAll();
    }
}
