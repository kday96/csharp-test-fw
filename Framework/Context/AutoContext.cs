using AutomationTests.Framework.Utilities;
using AventStack.ExtentReports;

namespace AutomationTests.Framework.Context;

public static class AutoContext
{
    public static ExtentTest? CurrentTest { get; set; }
    public static IWebDriver? Driver { get; set; }
    public static ScreenshotHelper? ScreenshotHelper { get; set; }
}