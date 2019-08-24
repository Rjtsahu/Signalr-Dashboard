using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.Core.Message
{
    internal class RequestQueryCollection
    {

        public string ClientProtocol { get; set; }

        public string ConnectionData { get; set; }

        public string Transport { get; set; }

        public string ConnectionToken { get; set; }

        protected string QueryString { get; set; }

        protected Dictionary<string, string> QueryCollection { get; private set; }

        public List<ConnectionDataParameter> ConnectionDatas { get; private set; }

        protected RequestQueryCollection(Uri requestUri)
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


        private void Decode()
        {
            var fields = this.GetType().GetProperties().ToList();
            foreach (var fieldInfo in fields)
            {
                try
                {
                    if (QueryCollection.ContainsKey(fieldInfo.Name.ToLower()))
                    {
                        fieldInfo.SetValue(this, Convert.ChangeType(QueryCollection[fieldInfo.Name.ToLower()], fieldInfo.PropertyType));
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
