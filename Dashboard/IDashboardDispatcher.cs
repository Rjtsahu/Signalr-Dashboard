using System.Threading.Tasks;

namespace SignalrDashboard.Dashboard
{
    internal interface IDashboardDispatcher
    {
        Task Dispatch(DashboardContext dashboardContext);
    }
}
