using System.Threading.Tasks;

namespace SignalrDashboard.DataStore.Dto
{
    internal class HubDataDto : IDataTableObject
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string HubName { get; set; }
        public string MethodName { get; set; }
        public string Arguments { get; set; }
        public string ReturnData { get; set; }
        public string ExceptionData { get; set; }

        public string TableName => "HubData";

        public bool Save(ISqlOperation sqlOperation)
        {
            var rowAdded = sqlOperation.Execute(ExecuteSqlQuery.InsertRow_HubData, RequestId, HubName,
                 MethodName, Arguments, ReturnData, ExceptionData);

            return rowAdded == 1;
        }

        public async Task<bool> SaveAsync(ISqlOperation sqlOperation)
        {
            var rowAdded = await sqlOperation.ExecuteAsync(ExecuteSqlQuery.InsertRow_HubData, RequestId, HubName,
                 MethodName, Arguments, ReturnData, ExceptionData);

            return rowAdded == 1;
        }
    }
}
