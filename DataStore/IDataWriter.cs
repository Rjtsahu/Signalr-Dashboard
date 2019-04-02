
namespace Sahurjt.Signalr.Dashboard.DataStore
{
    interface IDataWriter<T>
    {
        void Add(T data);
        bool Update(string id, T data);
        bool Delete(string id);
    }
}
