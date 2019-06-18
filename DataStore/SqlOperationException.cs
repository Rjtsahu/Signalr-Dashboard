using System;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal class SqlOperationException : Exception
    {
        private const string _exceptionString = "Database operation Exeption : ";

        public SqlOperationException() : base(_exceptionString)
        {

        }

        public override string ToString()
        {
            return $"{_exceptionString}  {base.ToString()} | Trace : {StackTrace} | Inner Exception : {InnerException?.StackTrace}";
        }
    }
}
