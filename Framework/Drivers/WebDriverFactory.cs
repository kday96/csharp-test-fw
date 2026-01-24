using Serilog;

namespace AutomationTests.Framework.Drivers;

public class WebDriverFactory
{
    public IWebDriver CreateDriver(BrowserType browser)
    {
        Log.Information("Starting WebDriver for browser: {Browser}", browser);
        IWebDriver driver = browser switch
        {
            BrowserType.Chrome => CreateChromeDriver(),
            BrowserType.Firefox => CreateFirefoxDriver(),
            BrowserType.Edge => CreateEdgeDriver(),
            BrowserType.Safari => CreateSafariDriver(),
            _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null)
        };
        driver.Manage().Window.Maximize();
        Log.Information("WebDriver for browser: {Browser} started successfully", browser);
        return driver;
    }


    private IWebDriver CreateChromeDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-notifications");
        options.AddArgument("disable-popup-blocking");
        return new ChromeDriver(options);
    }

    private IWebDriver CreateFirefoxDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");
        return new EdgeDriver(options);
    }

    private IWebDriver CreateEdgeDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");
        return new EdgeDriver(options);
    }

    private IWebDriver CreateSafariDriver()
    {
        var options = new SafariOptions();
        options.AddAdditionalOption("start-maximized", true);
        return new SafariDriver(options);
    }
}