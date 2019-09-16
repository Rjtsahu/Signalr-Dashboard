# Signalr-Dashboard (In Development)

## Web Api Middleware for monitoring Signalr connections.

### Introduction.
* There were times with me when we were using signalr in some project, there were some frequent issues with connection at client side, debugging there issue with default transport logs didn't provide much help.
* The scope of this project is to develop a dashboard which will have all clients/connection information in graphical form to make debugging and performance improvements.
* The idea of this project is to create a wrpper around Microsoft Signalr (using owin extension middleware)

### Goals
* Creating middleware pipeline using Owin.
* Intercepting request (only filtered as signalr) and stoing in sqlite database.
* Dashboard middleware to overide response of api in /dashboard (for various dashboard GET API)
* Default sql provider is sqlite database which can be extended to use any other database provider.
* Intercepting and analysing request and response on all three transports (websocket,serverSentEvents,longPolling)

### What's not in current goal ?
* Intercepting incoming and outgoing message when transport is websocket or serverSentEvents.

### Current Development Status :
* Intercepting part of code development is completing with ~100% unit test coverage.

### How to build / use
* Clone the repo
* Create a solution with a simple WebApi(including signalr dependency and client side logic) project.
* Import this project in your solution.
* In Start file (owin file) add following line before registering signalr middleware.
```C#
// some config related to library
DashboardGlobal.Configuration.FlushOldRecordAfter = TimeSpan.FromDays(2);

// register dashboard middleware
app.UseSignalrDashboard();

// register signalr middleware
var hubConfiguration = new HubConfiguration
{
    EnableDetailedErrors = true,
    EnableJavaScriptProxies = true
};

app.MapSignalR(hubConfiguration);
```