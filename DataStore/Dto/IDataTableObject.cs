using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal interface IDataTableObject
    {
        string TableName { get; }

        bool Save(ISqlOperation sqlOperation);

        Task<bool> SaveAsync(ISqlOperation sqlOperation);
    }
}
