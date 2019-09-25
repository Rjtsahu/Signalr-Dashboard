using System.Threading.Tasks;

namespace SignalrDashboard.DataStore.Dto
{
    internal class SessionDto : IDataTableObject
    {
        public int SessionId { get; set; }
        public string ConnectionToken { get; set; }
        public string ConnectionId { get; set; }
        public bool IsCompleted { get; set; }
        public long StartTimeStamp { get; set; }
        public long FinishTimeStamp { get; set; }
        public string NegotiateData { get; set; }

        public string TableName => "Session";

        public bool Save(ISqlOperation sqlOperation)
        {
            var rowAdded = sqlOperation.Execute(ExecuteSqlQuery.InsertRow_Session, ConnectionId, ConnectionToken,
                 IsCompleted, StartTimeStamp, FinishTimeStamp, NegotiateData);

            return rowAdded == 1;
        }

        public async Task<bool> SaveAsync(ISqlOperation sqlOperation)
        {
            var rowAdded = await sqlOperation.ExecuteAsync(ExecuteSqlQuery.InsertRow_Session, ConnectionId, ConnectionToken,
                  IsCompleted, StartTimeStamp, FinishTimeStamp, NegotiateData);

            return rowAdded == 1;
        }
    }
}
