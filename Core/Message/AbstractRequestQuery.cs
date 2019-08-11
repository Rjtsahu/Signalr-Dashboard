using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahurjt.Signalr.Dashboard.Core.Message
{
    internal abstract class AbstractRequestQuery
    {
        public string ClientProtocol { get; set; }

        public string ConnectionData { get; set; }

        protected string QueryString { get; set; }

        protected Dictionary<string, string> QueryCollection { get; private set; }

        public abstract RequestType RequestQueryType { get; }

        public List<ConnectionDataParameter> ConnectionDatas { get; private set; }

        protected AbstractRequestQuery(Uri requestUri)
        {
            QueryString = requestUri.Query;
            ParseQueryCollection();
            Decode();
        }

        protected void ParseQueryCollection()
        {
            if (string.IsNullOrEmpty(QueryString)) return;

            QueryCollection = new Dictionary<string, string>();

            var queryCollection = HttpUtility.ParseQueryString(QueryString);

            foreach (var key in queryCollection.AllKeys)
            {
                QueryCollection.Add(key.ToLower(), queryCollection.Get(key));
            }
        }

        protected abstract object GetObject();

        private void Decode()
        {
            var fields = GetObject()?.GetType().GetProperties().ToList();
            foreach (var fieldInfo in fields)
            {
                try
                {
                    if (QueryCollection.ContainsKey(fieldInfo.Name.ToLower()))
                    {
                        fieldInfo.SetValue(GetObject(), Convert.ChangeType(QueryCollection[fieldInfo.Name.ToLower()], fieldInfo.PropertyType));
                    }
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(ConnectionData))
            {
                ConnectionDatas = JsonConvert.DeserializeObject<List<ConnectionDataParameter>>(ConnectionData);
            }
        }

    }
}
