using AutomationTests.Framework.Logging;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Serilog;

namespace AutomationTests.Framework.Reporting;

public static class MyExtentReport
{
    private static ExtentReports? _extent;
    
    public static void InitialiseReport()
    {
        var reportsScreenshotsRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Reports_Screenshots");

        var extentReportPath = Path.Combine(reportsScreenshotsRoot, "Reports");
        var screenshotPath = Path.Combine(reportsScreenshotsRoot, "Screenshots");
        Directory.CreateDirectory(extentReportPath);
        Directory.CreateDirectory(screenshotPath);

        string reportFileName = $"ExtentReport_{DateTime.Now:dd-MM-yy_HH-mm}.html";
        var htmlReporter = new ExtentSparkReporter(Path.Combine(extentReportPath, reportFileName));
        _extent = new ExtentReports();
        _extent.AttachReporter(htmlReporter);

        LoggerConfig.Configure();
        Log.Information("======== Test Run Started ========");
    }

    public static ExtentTest? InitialiseTest()
    {
        var fullClassName = TestContext.CurrentContext.Test.ClassName;
        var fullClassNameParts = fullClassName.Split('.');
        
        var testClassName = fullClassNameParts.Length > 3 ? fullClassNameParts.Last() : string.Empty;
        var testName = TestContext.CurrentContext.Test.Name;

        var formattedTestName = (!string.IsNullOrEmpty(testClassName)) ? $"{testClassName} - {testName}" : testName;
        var description = TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString() ?? string.Empty;

        var test = _extent?.CreateTest(formattedTestName, description);

        return test;
    }

    public static void AttachScreenshotToReport(ExtentTest test, string screenshotPath)
    {
        if (string.IsNullOrEmpty(screenshotPath)) return;
        var fileName = Path.GetFileName(screenshotPath);
        var relativePath = Path.Combine("..", "Screenshots", fileName);
        test?.AddScreenCaptureFromPath(relativePath);
    }

    public static void AttachScreenshotToStep(ExtentTest node, string screenshotPath)
    {
        if (string.IsNullOrEmpty(screenshotPath)) return;
        var fileName = Path.GetFileName(screenshotPath);
        var relativePath = Path.Combine("..", "Screenshots", fileName);
        node?.AddScreenCaptureFromPath(relativePath);
    }

    public static void CloseReport()
    {
        Log.Information("==== Test Run Finished ====");
        Log.CloseAndFlush();
        _extent?.Flush();
    }
}