using System;
using System.Threading.Tasks;
using System.Web;

namespace SignalrDashboard.Dashboard.Dispatchers
{
    internal class RawContentDispatcher : IDashboardDispatcher
    {

        private readonly string _contentType;
        private readonly string _resourcePath;
        private readonly string _resourceName;

        public RawContentDispatcher(string contentType, string resourcePath)
        {
            _contentType = contentType;
            _resourcePath = resourcePath;
            _resourceName = null;
        }

        public RawContentDispatcher(string contentType, string resourcePath, string resourceName)
        {
            _contentType = contentType;
            _resourcePath = resourcePath;
            _resourceName = resourceName;
        }

        public async Task Dispatch(DashboardContext dashboardContext)
        {
            var resourceName = GetUrlDecodedResourceFileName(dashboardContext.Request.Path);

            dashboardContext.Response.ContentType = _contentType;
            dashboardContext.Response.SetExpire(DateTimeOffset.Now.AddMonths(1));


            using (var inputStream = GetType().Assembly.GetManifestResourceStream($"{_resourcePath}.{resourceName}"))
            {
                if (inputStream == null)
                {
                    throw new ResourceNotExistException($"{_resourcePath}.{resourceName}");
                }

                await inputStream.CopyToAsync(dashboardContext.Response.Body).ConfigureAwait(false);
            }
        }

        private string GetResourceFileName(string urlPath)
        {
            if (!string.IsNullOrEmpty(_resourceName)) return _resourceName;

            // Get resource name for request query.
            return urlPath.Substring(urlPath.LastIndexOf('/') + 1);
        }

        private string GetUrlDecodedResourceFileName(string urlPath)
        {
            // replace . in request url with @ ie %40
            return GetResourceFileName(HttpUtility.UrlDecode(urlPath))?.Replace('@', '.');
        }
    }
}
