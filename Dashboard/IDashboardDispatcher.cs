using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.Dashboard
{
    internal interface IDashboardDispatcher
    {
        Task Dispatch(DashboardContext dashboardContext);
    }
}
