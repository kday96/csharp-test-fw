using AutomationTests.Framework.Drivers;
using AutomationTests.Config;
using AutomationTests.Framework.Utilities;
using AutomationTests.Framework.Reporting;
using AventStack.ExtentReports;
using AutomationTests.Framework.Context;

namespace AutomationTests.Tests;

public class BaseTest
{
    protected IWebDriver? _driver;
    protected ScreenshotHelper? _screenshotHelper;
    protected ExtentTest? _test;
    protected CustomAssert? AssertK;

    [SetUp]
    public void SetUp()
    {
        _test = MyExtentReport.InitialiseTest();
        AutoContext.CurrentTest = _test;
        AssertK = new CustomAssert(_test);

        var browser = Enum.Parse<BrowserType>(ConfigReader.Settings.Browser, true);;
        _driver = new WebDriverFactory().CreateDriver(browser);
        AutoContext.Driver = _driver;

        AssertK.Log("Navigatng to Swag Labs URL");
        _driver.Navigate().GoToUrl(ConfigReader.Settings.BaseUrl);

        _screenshotHelper = new ScreenshotHelper(_driver);
        AutoContext.ScreenshotHelper = _screenshotHelper;
    }

    [TearDown]
    public void TearDown()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        var screenshotPath = _screenshotHelper?.CaptureScreenshot(TestContext.CurrentContext.Test.Name);

        if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            if (_test != null && screenshotPath != null)
            {
                MyExtentReport.AttachScreenshotToReport(_test, screenshotPath);
                _test.Fail("Test failed. Screenshot attached.");
            }
        }
        else
        {
            if (screenshotPath != null && _test != null) 
            {
                MyExtentReport.AttachScreenshotToReport(_test, screenshotPath);
                _test?.Pass("Test passed. Screenshot attached.");
            }
        }

        if (_driver != null)
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}