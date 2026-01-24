using Serilog;

namespace AutomationTests.Framework.Logging;

public static class LoggerConfig
{
    private static string GetProjectRootPath()
    {
        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    }
    public static void Configure()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Async(a => a.Console())
            .WriteTo.Async(a => a.File(
                Path.Combine(GetProjectRootPath(), "Tests", "Logs", "framework_log_.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10
            )).CreateLogger();

        Log.Information("Logger iniialised.");
    }
}