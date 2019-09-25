using System;

namespace SignalrDashboard.DataStore
{
    internal class SqlOperationException : Exception
    {
        private const string _exceptionString = "Unknown database exception";

        public string ExceptionMessage { get; }

        public SqlOperationException() : base(_exceptionString)
        {
            ExceptionMessage = _exceptionString;
        }


        public SqlOperationException(string exceptionMessage, Exception sourceException) : base(exceptionMessage,sourceException)
        {
            ExceptionMessage = exceptionMessage;
        }

        public override string ToString()
        {
            return $"{ExceptionMessage}  {base.ToString()} | Trace : {StackTrace} | Inner Exception : {InnerException?.StackTrace}";
        }
    }
}
