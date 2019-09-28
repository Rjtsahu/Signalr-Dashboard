using System;

namespace SignalrDashboard.Dashboard
{
    internal class ResourceNotExistException : Exception
    {
        public readonly string _resourceName;
        public ResourceNotExistException(string resourceName)
        {
            _resourceName = resourceName;
        }

        public override string ToString()
        {
            return $"Couldn't find resource : {_resourceName}";
        }
    }
}
