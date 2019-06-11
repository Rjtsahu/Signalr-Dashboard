namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
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
    }
}
